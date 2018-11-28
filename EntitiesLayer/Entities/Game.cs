using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    public class Game : BaseEntity
    {
        public List<User> Users { get; set; }
        [NotMapped]
        public List<Card> Deck { get; set; }
        [NotMapped]
        public List<Card> DiscardPile { get; set; }
        public List<Round> Rounds { get; set; }

        public Game()
        {
            DateOfCreation = DateTime.Now;
            Deck = new List<Card>();
            DiscardPile = new List<Card>();
            Users = new List<User>();
        }
    }
}