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
        private IGameRepository gameRepository;
        private ICardRepository cardRepository;
        private DeckProvider deckProvider;

        public GameService(IGameRepository gameRepository, ICardRepository cardRepository)
        {
            this.gameRepository = gameRepository;
            this.cardRepository = cardRepository;
            deckProvider = new DeckProvider();
        }
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
        private bool IsBlackJack(Move move)
        {
            if(move.Cards.Sum(x => x.CardValue) == 21 && move != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsBust(Move move)
        {
            if(move.Cards.Sum(x => x.CardValue) > 21 && move != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<GameViewModel> CreateNewGame(User user, int botQuantity)
        {
            var game = new Game();
            var cards = await cardRepository.Get();
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
                    game.Users.Add(new User { Nickname = "Bot#"+i, UserRole = UserRole.BotPlayer});
                }
            }

            game.Rounds.Add(new Round());
            await gameRepository.Create(game);
            deckProvider.Add(new Deck { Cards = game.Deck, DiscardPile = game.DiscardPile });

            return Mapper.Map<GameViewModel>(game);
        }
        public async Task<GameViewModel> CreateNewRound(int gameId)
        {
            var game = await gameRepository.Get(gameId);
            game.Rounds.Add(new Round { Game = game });
            await gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        public async Task<GameViewModel> DealCards(int gameId)
        {
            var game = await gameRepository.Get(gameId);
            var deckFromCache = deckProvider.Get();
            game.Deck = deckFromCache.Cards;
            game.DiscardPile = deckFromCache.DiscardPile;
            var cardToUser = new List<Card>();
            var dealer = game.Users.FirstOrDefault(x => x.UserRole == UserRole.Dealer);
            
            for (int i = 0; i < game.Users.Count; i++)
            {
                var move = new Move();
                if (game.Users[i].UserRole != UserRole.Dealer && game.Users[i].UserRole != UserRole.None)
                {
                    cardToUser = game.Deck.Skip(game.Deck.Count - 2).ToList();
                    game.Users[i].Cards.AddRange(cardToUser);
                    game.Deck.RemoveRange(game.Deck.Count - 2, 2);
                    move = new Move { Cards = game.Users[i].Cards, User = game.Users[i], Round = game.Rounds.Last() };
                    game.Rounds.Last().Moves.Add(move);
                }
                if(IsBlackJack(move))
                {
                    game.Rounds.Last().Moves.Last().IsWin = true;
                }
            }

            cardToUser = game.Deck.Skip(game.Deck.Count - 2).ToList();
            game.Users.FirstOrDefault(x => x.UserRole == UserRole.PeoplePlayer).Cards.AddRange(cardToUser);
            game.Deck.RemoveRange(game.Deck.Count - 2, 2);
            game.Rounds.Last().Moves.Add(new Move { Cards = dealer.Cards, User = dealer, Round = game.Rounds.Last() });

            await gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        public async Task<GameViewModel> DealCardToPlayer(User user, int gameId)
        {
            var game = await gameRepository.Get(gameId);
            var deckFromCache = deckProvider.Get();
            game.Deck = deckFromCache.Cards;
            game.DiscardPile = deckFromCache.DiscardPile;
            var move = new Move();
            if (user.UserRole != UserRole.None && user != null)
            {
                game.Users.FirstOrDefault(x => x.Id == user.Id).Cards.Add(game.Deck.Last());
                game.Deck.Remove(game.Deck.Last());
                move = new Move { Cards = user.Cards, User = user, Round = game.Rounds.Last() };
                game.Rounds.Last().Moves.Add(move);
            }
            await gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
    }
}