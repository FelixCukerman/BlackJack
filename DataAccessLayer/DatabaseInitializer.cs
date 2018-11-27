using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntitiesLayer.Entities;

namespace DataAccessLayer
{
    class DatabaseInitializer : CreateDatabaseIfNotExists<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = (int)Value.Two; j < (int)Value.Ten + 1; j++)
                {
                    context.Cards.Add(new Card { Suit = (Suit)i, Value = (Value)j, Key = j });
                }
                context.Cards.Add(new Card { Suit = (Suit)i, Value = Value.Jack, Key = 10 });
                context.Cards.Add(new Card { Suit = (Suit)i, Value = Value.Queen, Key = 10 });
                context.Cards.Add(new Card { Suit = (Suit)i, Value = Value.King, Key = 10 });
                context.Cards.Add(new Card { Suit = (Suit)i, Value = Value.Ace, Key = 11 });
            }
        }
    }
}
