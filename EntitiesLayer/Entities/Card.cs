﻿using System;
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
        public int CardValue { get; set; }
        [Required]
        public CardName CardName { get; set; }
        public List<Move> Moves { get; set; }

        public Card()
        {
            DateOfCreation = DateTime.Now;
            Suit = Suit.None;
            CardValue = -1;
            CardName = CardName.None;
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

    public enum CardName
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
