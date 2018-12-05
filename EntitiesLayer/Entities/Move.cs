using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    public class Move : BaseEntity
    {
        public Round Round { get; set; }
        public User User { get; set; }
        public List<MoveCards> MoveCards { get; set; }

        public Move()
        {
            Id = -1;
            DateOfCreation = DateTime.Now;
            Round = new Round();
            User = new User();
            MoveCards = new List<MoveCards>();
        }
    }
}
