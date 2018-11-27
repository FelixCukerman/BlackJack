using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    [NotMapped]
    public class Game : BaseEntity
    {
        public List<Card> Deck { get; set; }
        public List<User> Users { get; set; }

        public Game()
        {
            DateOfCreation = DateTime.Now;
            Deck = new List<Card>();
            Users = new List<User>();
        }
    }
}