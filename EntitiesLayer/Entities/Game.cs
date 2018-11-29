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
        [NotMapped]
        public List<Card> Deck { get; set; } //from cache
        [NotMapped]
        public List<Card> DiscardPile { get; set; } //from User.Cards
        public List<Round> Rounds { get; set; }
        public List<User> Users { get; set; }

        public Game()
        {
            DateOfCreation = DateTime.Now;
            Deck = new Deck().Cards;
            DiscardPile = new List<Card>();
            Rounds = new List<Round>();
            Users = new List<User>();
        }
    }
}