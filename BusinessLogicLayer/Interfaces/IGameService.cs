using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.ViewModels;
using EntitiesLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGameService
    {
        Task<GameViewModel> CreateNewGame(User user, int botQuantity);
    }
}
