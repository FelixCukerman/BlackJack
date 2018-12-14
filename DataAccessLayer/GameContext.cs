using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntitiesLayer.Entities;

namespace DataAccessLayer
{
    public class GameContext : DbContext
    {
        static GameContext()
        {
            Database.SetInitializer<GameContext>(new DatabaseInitializer());
        }

        public GameContext() : base("gamedb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<MoveCards> MoveCards { get; set; }
        public DbSet<UserGames> UserGames { get; set; }
        public DbSet<UserRound> UserRounds { get; set; }
    }
}
