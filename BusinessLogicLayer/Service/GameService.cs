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

namespace BusinessLogicLayer.Service
{
    public class GameService : IGameService
    {
        IGameRepository gameRepository;
        IUserRepository userRepository;
        IMoveRepository moveRepository;
        IRoundRepository roundRepository;

        public GameService(IGameRepository gameRepository, IUserRepository userRepository, IMoveRepository moveRepository, IRoundRepository roundRepository)
        {
            this.gameRepository = gameRepository;
            this.userRepository = userRepository;
            this.moveRepository = moveRepository;
            this.roundRepository = roundRepository;
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
        public async Task<GameViewModel> CreateNewGame(User user, int botQuantity)
        {
            var game = new Game();
            game.Deck = this.Reshuffle(game.Deck);

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

            return Mapper.Map<GameViewModel>(game);
        }
        public async Task<GameViewModel> CreateNewRound(int gameId)
        {
            var game = await gameRepository.Get(gameId);
            game.Rounds.Add(new Round());
            await gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        public async Task<GameViewModel> DealCards(int gameId)
        {
            var game = await gameRepository.Get(gameId);
            var dealer = game.Users.FirstOrDefault(x => x.UserRole == UserRole.Dealer);

            for (int i = 0; i < game.Users.Count; i++)
            {
                if(game.Users[i].UserRole != UserRole.Dealer && game.Users[i].UserRole != UserRole.None)
                {
                    for(int j = 0; j < 2; j++)
                    {
                        game.Users[i].Cards.Add(game.Deck.Last());
                        game.Deck.Remove(game.Deck.Last());
                        game.Rounds.Last().Moves.Add(new Move { Cards = game.Users[i].Cards, User = game.Users[i], Round = game.Rounds.Last()});
                    }
                }
            }
            for (int j = 0; j < 2; j++)
            {
                dealer.Cards.Add(game.Deck.Last());
                game.Deck.Remove(game.Deck.Last());
                game.Rounds.Last().Moves.Add(new Move { Cards = dealer.Cards, User = dealer, Round = game.Rounds.Last() });
            }
            await gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
        public async Task<GameViewModel> DealCardToPlayer(User user, int gameId)
        {
            var game = await gameRepository.Get(gameId);

            if (user.UserRole != UserRole.None && user != null)
            {
                game.Users.FirstOrDefault(x => x.Id == user.Id).Cards.Add(game.Deck.Last());
                game.Deck.Remove(game.Deck.Last());
                game.Rounds.Last().Moves.Add(new Move { Cards = user.Cards, User = user, Round = game.Rounds.Last() });
            }
            await gameRepository.Update(game);
            return Mapper.Map<GameViewModel>(game);
        }
    }
}