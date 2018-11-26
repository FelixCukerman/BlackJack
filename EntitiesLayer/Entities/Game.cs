using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLayer.Entities
{
    [NotMapped]
    public class Game
    {
        public int Id { get; set; }
        public List<Card> Deck { get; set; }
        public List<User> Users { get; set; }

        public Game()
        {
            InitializeDeck();
            Users = new List<User>();
        }

        private void InitializeDeck()
        {
            Deck = new List<Card>();

            for(int i = 1; i < 5; i++)
            {
                Deck.Add(new Card { Suit = (Suit)i, Value = Value.Ace, Key = 11 });
                Deck.Add(new Card { Suit = (Suit)i, Value = Value.Jack, Key = 10 });
                Deck.Add(new Card { Suit = (Suit)i, Value = Value.Queen, Key = 10 });
                Deck.Add(new Card { Suit = (Suit)i, Value = Value.King, Key = 10 });

                for (int j = 2; j < 11; j++)
                {
                    Deck.Add(new Card { Suit = (Suit)i, Value = (Value)j });
                }
            }
        }
    }
}