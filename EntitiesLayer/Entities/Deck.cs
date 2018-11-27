using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace EntitiesLayer.Entities
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = (int)Value.Two; j < (int)Value.Ten + 1; j++)
                {
                    Cards.Add(new Card { Suit = (Suit)i, Value = (Value)j, Key = j });
                }
                Cards.Add(new Card { Suit = (Suit)i, Value = Value.Jack, Key = 10 });
                Cards.Add(new Card { Suit = (Suit)i, Value = Value.Queen, Key = 10 });
                Cards.Add(new Card { Suit = (Suit)i, Value = Value.King, Key = 10 });
                Cards.Add(new Card { Suit = (Suit)i, Value = Value.Ace, Key = 11 });
            }
        }
    }
}
