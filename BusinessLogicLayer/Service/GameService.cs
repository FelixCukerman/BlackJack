using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using EntitiesLayer.Entities;
using ViewModelsLayer.ViewModels;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using DataAccessLayer;
using AutoMapper;
using BusinessLogicLayer.Providers;

namespace BusinessLogicLayer.Service
{
    public class GameService : IGameService
    {
        #region Fields
        private IGameRepository _gameRepository;
        private ICardRepository _cardRepository;
        private DeckProvider _deckProvider;
        #endregion

        #region Constructor
        public GameService(IGameRepository gameRepository, ICardRepository cardRepository)
        {
            this._gameRepository = gameRepository;
            this._cardRepository = cardRepository;
            _deckProvider = new DeckProvider();
        }
        #endregion

        #region Reshuffle
        private List<Card> Reshuffle(List<Card> Deck)
        {
            Random rand = new Random();
            for (int i = Deck.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                Card tmp = Deck[j];
                Deck[j] = Deck[i];
                Deck[i] = tmp;
            }
            return Deck;
        }
        #endregion

        #region CheckDeck
        private Game CheckDeck(Game game)
        {
            if(game.Deck.Count < 20)
            {
                game.Deck.AddRange(game.DiscardPile);
                game.DiscardPile.RemoveRange(0, game.DiscardPile.Count);
                _deckProvider.Update(new Deck { Cards = game.Deck, DiscardPile = game.DiscardPile });
            }
            return game;
        }
        #endregion

        #region IsBlackJack
        private bool IsBlackJack(Move move)
        {
            if(move.Cards.Sum(x => x.CardValue) == 21 && move != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region IsBust
        private bool IsBust(Move move)
        {
            if(move.Cards.Sum(x => x.CardValue) > 21 || move != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region RoundIsOver
        private bool RoundIsOver(Game game)
        {
            if(game.Rounds.Last().Moves.Where(x => x.IsWin != null).Count() == game.Users.Count())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region CreateNewGame
        public async Task<GameViewModel> CreateNewGame(User user, int botQuantity)
        {
            var game = new Game();
            var cards = await _cardRepository.Get();
            game.Deck = this.Reshuffle(cards.ToList());

            if (user.UserRole == UserRole.PeoplePlayer && user != null)
            {
                game.Users.Add(user);
            }
            if(botQuantity >= 0 && botQuantity <= 5)
            {
                game.Users.Add(new User { Nickname = "Dealer", UserRole = UserRole.Dealer });
                for (int i = 0; i < botQuantity; i++)
                {
                    game.Users.Add(new User { Nickname = "Bot#"+(i+1), UserRole = UserRole.BotPlayer});
                }
            }

            game.Rounds.Add(new Round());
            await _gameRepository.Create(game);
            _deckProvider.Add(new Deck { Cards = game.Deck, DiscardPile = game.DiscardPile });

            return Mapper.Map<GameViewModel>(game);
        }
        #endregion

        #region CreateNewRound
        public async Task<GameViewModel> CreateNewRound(int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            var deckFromCache = _deckProvider.Get();
            game.Rounds.Add(new Round { Game = game });
            game.Deck = deckFromCache.Cards;
            game.DiscardPile = deckFromCache.DiscardPile;
            game = CheckDeck(game);
            await _gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        #endregion

        #region DealCards
        public async Task<GameViewModel> DealCards(int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            var deckFromCache = _deckProvider.Get();
            game.Deck = deckFromCache.Cards;
            game.DiscardPile = deckFromCache.DiscardPile;
            var cardToUser = new List<Card>();
            var move = new Move();
            var dealer = game.Users.FirstOrDefault(x => x.UserRole == UserRole.Dealer);
            var losers = new List<User>();
            var winers = new List<User>();
            for (int i = 0; i < game.Users.Count; i++)
            {
                if (game.Users[i].UserRole != UserRole.Dealer && game.Users[i].UserRole != UserRole.None)
                {
                    cardToUser = game.Deck.Skip(game.Deck.Count - 2).ToList();
                    game.Users[i].Cards.AddRange(cardToUser);
                    game.Deck.RemoveRange(game.Deck.Count - 2, 2);
                    move = new Move { Cards = game.Users[i].Cards, User = game.Users[i], Round = game.Rounds.Last() };
                    game.Rounds.Last().Moves.Add(move);
                }
                if (IsBlackJack(move))
                {
                    move.IsWin = true;
                }
            }
            cardToUser = game.Deck.Skip(game.Deck.Count - 2).ToList();
            dealer.Cards.AddRange(cardToUser);
            game.Deck.RemoveRange(game.Deck.Count - 2, 2);
            move = new Move { Cards = dealer.Cards, User = dealer, Round = game.Rounds.Last() };
            game.Rounds.Last().Moves.Add(move);
            _deckProvider.Update(new Deck { Cards = game.Deck, DiscardPile = new List<Card>()});
            if (IsBlackJack(move))
            {
                move.IsWin = true;
                for (int i = 0; i < game.Rounds.Last().Moves.Count; i++)
                {
                    winers.Add(game.Rounds.Last().Moves.Where(x => x.IsWin == true && x.IsWin != null).ElementAt(i).User);
                }
                losers = game.Users.Except(winers).ToList();
                for (int i = 0; i < losers.Count; i++)
                {
                    losers[i].Games.FirstOrDefault(x => x.Id == game.Id).Rounds.Last().Moves.Last().IsWin = false;
                }
            }
            await _gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        #endregion

        #region DealCardToPlayer
        public async Task<GameViewModel> DealCardToPlayer(User user, int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            var deckFromCache = _deckProvider.Get();
            var move = new Move();
            game.Deck = deckFromCache.Cards;
            game.DiscardPile = deckFromCache.DiscardPile;

            if (game.Rounds.Last().Moves.Where(x => x.IsWin != null).Any(x => x.User.Id == user.Id))
            {
                return Mapper.Map<GameViewModel>(game);
            }

            if (user.UserRole == UserRole.PeoplePlayer && user != null)
            {
                game.Users.FirstOrDefault(x => x.Id == user.Id).Cards.Add(game.Deck.Last());
                game.Deck.Remove(game.Deck.Last());
                move = new Move { Cards = user.Cards, User = user, Round = game.Rounds.Last() };
                game.Rounds.Last().Moves.Add(move);
            }

            if(IsBust(move))
            {
                move.IsWin = false;
            }

            _deckProvider.Update(new Deck { Cards = game.Deck, DiscardPile = new List<Card>() });
            await _gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        #endregion

        #region DealCardToDealer
        public async Task<GameViewModel> DealCardToDealer(int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            var deckFromCache = _deckProvider.Get();
            game.Deck = deckFromCache.Cards;
            game.DiscardPile = deckFromCache.DiscardPile;
            var dealer = game.Users.FirstOrDefault(x => x.UserRole == UserRole.Dealer);

            if(dealer.Cards.Sum(x => x.CardValue) > 17)
            {
                return Mapper.Map<GameViewModel>(game);
            }

            dealer.Cards.Add(game.Deck.Last());
            game.Deck.Remove(game.Deck.Last());
            game.Rounds.Last().Moves.Add(new Move { Cards = dealer.Cards, User = dealer, Round = game.Rounds.Last() });

            _deckProvider.Update(new Deck { Cards = game.Deck, DiscardPile = new List<Card>() });
            await _gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        #endregion

        #region DealCardToBots
        public async Task<GameViewModel> DealCardToBots(int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            var deckFromCache = _deckProvider.Get();
            game.Deck = deckFromCache.Cards;
            game.DiscardPile = deckFromCache.DiscardPile;
            var bots = game.Users.Where(x => x.UserRole == UserRole.BotPlayer);

            for(int i = 0; i < bots.Count(); i++)
            {
                if (game.Rounds.Last().Moves.Where(x => x.IsWin != null).Any(x => x.User.Id == bots.ElementAt(i).Id))
                {
                    return Mapper.Map<GameViewModel>(game);
                }

                if (new Random().Next(0, 2) == 0)
                {
                    return Mapper.Map<GameViewModel>(game);
                }

                User item = bots.ElementAt(i);
                item.Cards.Add(game.Deck.Last());
                game.Deck.Remove(game.Deck.Last());
                game.Rounds.Last().Moves.Add(new Move { Cards = item.Cards, User = item, Round = game.Rounds.Last() });
            }
            _deckProvider.Update(new Deck { Cards = game.Deck, DiscardPile = new List<Card>() });
            await _gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        #endregion

        #region GameHistory
        public async Task<GameHistoryViewModel> GameHistory(int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            return Mapper.Map<GameHistoryViewModel>(game);
        }
        #endregion
    }
}