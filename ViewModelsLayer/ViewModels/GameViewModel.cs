

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace ViewModelsLayer.ViewModels
{
    public class GameViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<CardViewModel> Deck { get; set; }
        public List<CardViewModel> DiscardPile { get; set; }
        public List<RoundViewModel> Rounds { get; set; }
        public bool IsOver { get; set; }
    }
}
