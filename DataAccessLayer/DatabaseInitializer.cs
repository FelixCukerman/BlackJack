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
            context.Cards.AddRange(new Deck().Cards);
            context.SaveChanges();
        }
    }
}
