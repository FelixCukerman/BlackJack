using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    public class Card : BaseEntity
    {
        [Required]
        public Suit Suit { get; set; }
        [Required]
        public int Key { get; set; }
        [Required]
        public Value Value { get; set; }
        public List<User> Users { get; set; }

        public Card()
        {
            Id = -1;
            DateOfCreation = DateTime.Now;
            Suit = Suit.None;
            Key = -1;
            Value = Value.None;
            Users = new List<User>();
        }
    }
    public enum Suit
    {
        None = 0,
        Hearts = 1,
        Tiles = 2,
        Clovers = 3,
        Pikes = 4
    }

    public enum Value
    {
        None = 0,
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
}
