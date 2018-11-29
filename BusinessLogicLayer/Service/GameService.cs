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
        GameContext context;
        //IGameRepository gameRepository = new GameRepository(context);
        Game game = new Game();
        public async Task<GameViewModel> CreateNewGame(User user, int botQuantity)
        {
            if(user.UserRole == UserRole.PeoplePlayer && user != null)
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
            return Mapper.Map<GameViewModel>(game);
        }
    }
}