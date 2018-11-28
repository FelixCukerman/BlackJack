using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace BusinessLogicLayer.ViewModels
{
    public class CardViewModel
    {
        public Suit Suit { get; set; }
        public int CardValue { get; set; }
        public CardName CardName { get; set; }
        public List<User> Users { get; set; }
    }
}
