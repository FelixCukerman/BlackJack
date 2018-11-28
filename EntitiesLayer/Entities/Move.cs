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
        public User User { get; set; }
        public List<Card> Cards { get; set; }
    }
}
