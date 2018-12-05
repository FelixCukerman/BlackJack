using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Abstraction;
using EntitiesLayer.Entities;

namespace EntitiesLayer.Entities
{
    public class MoveCards : BaseEntity
    {
        public Move Move { get; set; }
        public Card Card { get; set; }
        public MoveCards()
        {
            DateOfCreation = DateTime.Now;
            Move = new Move();
            Card = new Card();
        }
    }
}
