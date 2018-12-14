using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntitiesLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace DataAccessLayer
{
    class DatabaseInitializer : CreateDatabaseIfNotExists<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            List<Card> cards = new List<Card>();
            for (int i = 1; i < 5; i++)
            {
                for (int j = (int)CardName.Two; j < (int)CardName.Ten + 1; j++)
                {
                    cards.Add(new Card { Suit = (Suit)i, CardName = (CardName)j, CardValue = j });
                }
                cards.Add(new Card { Suit = (Suit)i, CardName = CardName.Jack, CardValue = 10 });
                cards.Add(new Card { Suit = (Suit)i, CardName = CardName.Queen, CardValue = 10 });
                cards.Add(new Card { Suit = (Suit)i, CardName = CardName.King, CardValue = 10 });
                cards.Add(new Card { Suit = (Suit)i, CardName = CardName.Ace, CardValue = 11 });
            }
            context.Cards.AddRange(cards);
            context.SaveChanges();
        }
    }
}
