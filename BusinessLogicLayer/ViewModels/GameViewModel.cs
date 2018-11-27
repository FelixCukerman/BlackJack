using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace BusinessLogicLayer.ViewModels
{
    public class GameViewModel
    {
        public List<Card> Deck { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<User> Users { get; set; }
    }
}
