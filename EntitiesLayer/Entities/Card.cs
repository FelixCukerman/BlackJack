using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntitiesLayer.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Suit Suit { get; set; }
        [Required]
        public string Value { get; set; }
        public User User { get; set; } 
    }
    public enum Suit
    {
        None = 0,
        Hearts = 1,
        Tiles = 2,
        Clovers = 3,
        Pikes = 4
    }

    //public enum Value
    //{
    //    Two = 2,
    //    Three = 3,
    //    Four = 4,
    //    Five = 5,
    //    Six = 6,
    //    Seven = 7,
    //    Eight = 8,
    //    Nine = 9,
    //    Ten = 10,
    //    Jack = 11,
    //    Queen = 12,
    //    King = 13,
    //}
}
