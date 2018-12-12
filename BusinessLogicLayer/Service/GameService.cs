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
    public class GameService/* : IGameService*/
    {
        #region Fields
        private IGameRepository _gameRepository;
        private ICardRepository _cardRepository;
        private IRoundRepository _roundRepository;
        private IMoveRepository _moveRepository;
        private IMoveCardsRepository _moveCardsRepository;
        private IUserGamesRepository _userGamesRepository;
        private IUserRepository _userRepository;
        private DeckProvider _deckProvider;
        #endregion

        #region Constructor
        public GameService(IGameRepository gameRepository, ICardRepository cardRepository, IRoundRepository roundRepository, IMoveRepository moveRepository, IMoveCardsRepository moveCardsRepository, IUserGamesRepository userGamesRepository, IUserRepository userRepository)
        {
            this._gameRepository = gameRepository;
            this._cardRepository = cardRepository;
            this._roundRepository = roundRepository;
            this._moveRepository = moveRepository;
            this._moveCardsRepository = moveCardsRepository;
            this._userGamesRepository = userGamesRepository;
            this._userRepository = userRepository;
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
        private Deck CheckDeck(Deck deck)
        {
            if (deck.Cards.Count < 15)
            {
                deck.Cards.AddRange(deck.DiscardPile);
                deck.DiscardPile.RemoveRange(0, deck.DiscardPile.Count);
                _deckProvider.Update(new Deck { Cards = deck.Cards, DiscardPile = deck.DiscardPile });
            }
            return deck;
        }
        #endregion

        #region IsBlackJack
        private async Task<bool> IsBlackJack(Move move)
        {
            var moveCards = await _moveCardsRepository.Get(x => x.MoveId == move.Id);
            var cards = new List<Card>();
            for(int i = 0; i < moveCards.Count(); i++)
            {
                cards.Add(await _cardRepository.Get(moveCards.ElementAt(i).CardId));
            }
            if (cards.Sum(x => x.CardValue) == 21)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region IsBust
        private async Task<bool> IsBust(Move move)
        {
            var moveCards = await _moveCardsRepository.Get(x => x.MoveId == move.Id);
            var cards = await MovecardsToCards(moveCards);
            if (cards.Sum(x => x.CardValue) > 21)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region RoundIsOver
        private async Task<bool> RoundIsOver(Game game)
        {
            var rounds = await _roundRepository.Get(x => x.GameId == game.Id);
            var moves = await _moveRepository.Get(x => x.Id == rounds.OrderBy(y => y.DateOfCreation).Last().Id);
            var user = await _userGamesRepository.Get(x => x.GameId == game.Id);

            if (moves.Where(x => x.IsWin != null).Count() == user.Count())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region GameIsOver
        public async Task<bool> GameIsOver(Game game)
        {
            var rounds = await _roundRepository.Get(x => x.GameId == game.Id);
            if (game.RoundQuantity == rounds.Count())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region UsergamesToUsers
        private async Task<List<User>> UsergamesToUsers(IEnumerable<UserGames> userGames)
        {
            var users = new List<User>();
            if (userGames != null)
            {
                for(int i = 0; i < userGames.Count(); i++)
                {
                    users.Add(await _userRepository.Get((int)userGames.ElementAt(i).UserId));
                }
            }
            return users;
        }
        #endregion

        #region MovecardsToCards
        private async Task<List<Card>> MovecardsToCards(IEnumerable<MoveCards> moveCards)
        {
            var cards = new List<Card>();
            if(moveCards != null)
            {
                for(int i = 0; i < moveCards.Count(); i++)
                {
                    cards.Add(await _cardRepository.Get((int)moveCards.ElementAt(i).MoveId));
                }
            }
            return cards;
        }
        #endregion

        #region SetLoserMoves
        private async Task SetLoserMoves(List<Move> moves)
        {
            List<Move> winersMove = new List<Move>();
            winersMove.AddRange(moves.Where(x => x.IsWin == true));
            List<Move> losersMove = moves.Except(winersMove).ToList();

            for (int i = 0; i < losersMove.Count; i++)
            {
                losersMove[i].IsWin = false;
            }
            await _moveRepository.UpdateRange(moves);
        }
        #endregion

        #region CreateNewGame
        public async Task<GameViewModel> CreateNewGame(User user, int botQuantity, int roundQuantity)
        {
            var game = new Game();
            var cards = await _cardRepository.Get();
            cards = this.Reshuffle(cards.ToList());
            var userGames = new List<UserGames>();
            var users = new List<User>();
            game.RoundQuantity = roundQuantity;

            if (user.UserRole == UserRole.PeoplePlayer && user != null)
            {
               userGames.Add(new UserGames { Game = game, User = user });
            }

            if (botQuantity >= 0 && botQuantity <= 5)
            {
                userGames.Add(new UserGames { Game = game, User = new User { Nickname = "Dealer", UserRole = UserRole.Dealer } });
                for (int i = 0; i < botQuantity; i++)
                {
                    userGames.Add( new UserGames { Game = game, User = new User { Nickname = "Bot#" + (i + 1), UserRole = UserRole.BotPlayer } });
                }
            }

            await _gameRepository.Create(game);
            await CreateNewRound(game.Id);
            await _userGamesRepository.CreateRange(userGames);
            _deckProvider.Add(new Deck { Cards = cards.ToList(), DiscardPile = new List<Card>() });

            for(int i = 0; i < userGames.Count; i++)
            {
                users.Add(await _userRepository.Get(userGames.ElementAt(i).User.Id));
            }

            GameViewModel result = new GameViewModel
            {
                Deck = Mapper.Map<List<CardViewModel>>(cards.ToList()),
                DiscardPile = new List<CardViewModel>(),
                IsOver = false,
                Rounds = Mapper.Map<List<RoundViewModel>>(await _roundRepository.Get(x => x.GameId == game.Id)),
                Users = Mapper.Map<List<UserViewModel>>(users)
            };

            return result;

        }
        #endregion

        #region CreateNewRound
        private async Task<Round> CreateNewRound(int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            var round = new Round { Game = game };
            await _roundRepository.Create(round);
            return round;
        }
        #endregion

        #region DealCards
        public async Task<GameViewModel> DealCards(int gameId)
        {
            var result = new GameViewModel();
            var deckFromCache = _deckProvider.Get();
            var cardToUser = new List<Card>();
            var move = new Move();
            var users = await UsergamesToUsers(await _userGamesRepository.Get(x => x.GameId == gameId));
            var rounds = await _roundRepository.Get(x => x.GameId == gameId);

            for (int i = 0; i < users.Count(); i++)
            {
                if (users.ElementAt(i).UserRole != UserRole.Dealer && users.ElementAt(i).UserRole != UserRole.None)
                {
                    cardToUser = deckFromCache.Cards.Skip(deckFromCache.Cards.Count - 2).ToList();
                    deckFromCache.Cards.RemoveRange(deckFromCache.Cards.Count - 2, 2);
                    move = new Move { RoundId = rounds.Last().Id, UserId = users.ElementAt(i).Id };
                    await _moveCardsRepository.CreateRange(new List<MoveCards> { new MoveCards { Move = move, Card = cardToUser[0]}, new MoveCards { Move = move, Card = cardToUser[1]} });
                }
                if (await IsBlackJack(move))
                {
                    move.IsWin = true;
                }
            }
            cardToUser = deckFromCache.Cards.Skip(deckFromCache.Cards.Count - 2).ToList(); //TODO: remove CARDTOUSER
            deckFromCache.Cards.RemoveRange(deckFromCache.Cards.Count - 2, 2);
            move = new Move { RoundId = rounds.Last().Id, UserId = users.FirstOrDefault(x => x.UserRole == UserRole.Dealer).Id };
            await _moveCardsRepository.CreateRange(new List<MoveCards> { new MoveCards { Move = move, Card = cardToUser[0] }, new MoveCards { Move = move, Card = cardToUser[1] } });
            _deckProvider.Update(new Deck { Cards = deckFromCache.Cards, DiscardPile = new List<Card>() });

            if (await IsBlackJack(move))
            {
                move.IsWin = true;
                result.IsOver = true;
                var movesAtCurrentRound = await _moveRepository.Get(x => x.RoundId == rounds.Last().Id);
                await SetLoserMoves(movesAtCurrentRound.ToList());
            }

            result.Deck = Mapper.Map<List<CardViewModel>>(deckFromCache.Cards);
            result.DiscardPile = Mapper.Map<List<CardViewModel>>(deckFromCache.DiscardPile);
            result.Rounds = Mapper.Map<List<RoundViewModel>>(rounds);
            result.Users = Mapper.Map<List<UserViewModel>>(users);

            return result;
        }
        #endregion

        #region GetRangeCard 
        private async Task<List<Card>> GetCardFromDB(List<Card> cards)
        {
            var dbcards = await _cardRepository.Get();
            var result = new List<Card>();

            foreach (var item in cards)
            {
                result.Add(dbcards.FirstOrDefault(x => x.Id == item.Id));
            }

            return result;
        }
        #endregion //Возможно стоит удалить

        #region DealCardToPlayer
        public async Task<GameViewModel> DealCardToPlayer(User user, int gameId)
        {
            var game = await _gameRepository.Get(gameId);
            var deckFromCache = _deckProvider.Get();
            var rounds = await _roundRepository.Get(x => x.GameId == gameId);
            var movesAtCurrentRound = await _moveRepository.Get(x => x.RoundId == rounds.Last().Id);
            var move = new Move();
            var result = new GameViewModel
            {
                Deck = Mapper.Map<List<CardViewModel>>(deckFromCache.Cards),
                DiscardPile = Mapper.Map<List<CardViewModel>>(deckFromCache.DiscardPile),
                IsOver = false,
                Rounds = Mapper.Map<List<RoundViewModel>>(rounds),
                Users = Mapper.Map<List<UserViewModel>>(await UsergamesToUsers(await _userGamesRepository.Get(x => x.GameId == gameId)))
            };

            if (movesAtCurrentRound.Where(x => x.IsWin != null).Any(x => x.User.Id == user.Id))
            {
                return result;
            }

            if (user.UserRole == UserRole.PeoplePlayer && user != null)
            {
                move = new Move { UserId = user.Id, RoundId = rounds.Last().Id };
                await _moveRepository.Create(move);
                await _moveCardsRepository.Create(new MoveCards { CardId = deckFromCache.Cards.Last().Id, MoveId = move.Id });
                deckFromCache.Cards.Remove(deckFromCache.Cards.Last());
                //move.Cards.AddRange(user.Cards);
                //game.Rounds.Last().Moves.Add(move);
            }

            if (await IsBust(move))
            {
                move.IsWin = false;
                await _moveRepository.Update(move);
            }

            _deckProvider.Update(new Deck { Cards = deckFromCache.Cards, DiscardPile = deckFromCache.DiscardPile });
            return result;
        }
        #endregion

        //#region DealCardToDealer
        //public async Task<GameViewModel> DealCardToDealer(int gameId)
        //{
        //    var game = await _gameRepository.Get(gameId);
        //    var deckFromCache = _deckProvider.Get();
        //    game.Deck = deckFromCache.Cards;
        //    game.DiscardPile = deckFromCache.DiscardPile;
        //    var dealer = game.Users.FirstOrDefault(x => x.UserRole == UserRole.Dealer);

        //    if(dealer.Cards.Sum(x => x.CardValue) > 17)
        //    {
        //        return Mapper.Map<GameViewModel>(game);
        //    }

        //    dealer.Cards.Add(game.Deck.Last());
        //    game.Deck.Remove(game.Deck.Last());
        //    game.Rounds.Last().Moves.Add(new Move { Cards = dealer.Cards, User = dealer, Round = game.Rounds.Last() });

        //    _deckProvider.Update(new Deck { Cards = game.Deck, DiscardPile = new List<Card>() });
        //    await _gameRepository.Update(game);

        //    if(GameIsOver(game))
        //    {
        //        var result = Mapper.Map<GameViewModel>(game);
        //        result.IsOver = true;
        //        return result;
        //    }

        //    if(RoundIsOver(game))
        //    {
        //        var result = await CreateNewRound(game.Id);
        //        return result;
        //    }
        //    return Mapper.Map<GameViewModel>(game);
        //}
        //#endregion

        //#region DealCardToBots
        //public async Task<GameViewModel> DealCardToBots(int gameId)
        //{
        //    var game = await _gameRepository.Get(gameId);
        //    var deckFromCache = _deckProvider.Get();
        //    game.Deck = deckFromCache.Cards;
        //    game.DiscardPile = deckFromCache.DiscardPile;
        //    var bots = game.Users.Where(x => x.UserRole == UserRole.BotPlayer);

        //    for(int i = 0; i < bots.Count(); i++)
        //    {
        //        if (game.Rounds.Last().Moves.Where(x => x.IsWin != null).Any(x => x.User.Id == bots.ElementAt(i).Id))
        //        {
        //            return Mapper.Map<GameViewModel>(game);
        //        }

        //        if (new Random().Next(0, 2) == 0)
        //        {
        //            return Mapper.Map<GameViewModel>(game);
        //        }

        //        User item = bots.ElementAt(i);
        //        item.Cards.Add(game.Deck.Last());
        //        game.Deck.Remove(game.Deck.Last());
        //        game.Rounds.Last().Moves.Add(new Move { Cards = item.Cards, User = item, Round = game.Rounds.Last() });
        //    }
        //    _deckProvider.Update(new Deck { Cards = game.Deck, DiscardPile = new List<Card>() });
        //    await _gameRepository.Update(game);
        //    return Mapper.Map<GameViewModel>(game);
        //}
        //#endregion

        //#region GameHistory
        //public async Task<GameHistoryViewModel> GameHistory(int gameId)
        //{
        //    var game = await _gameRepository.Get(gameId);
        //    return Mapper.Map<GameHistoryViewModel>(game);
        //}
        //#endregion
    }
}