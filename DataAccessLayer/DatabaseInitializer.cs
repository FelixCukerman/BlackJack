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
            Round round = new Round();

            cards = new Deck().Cards;

            context.Cards.AddRange(cards);
            context.Games.Add(new Game());
            context.Rounds.Add(new Round());
            context.Moves.Add(new Move());
            context.Users.Add(new User());

            context.SaveChanges();
        }
    }
}
