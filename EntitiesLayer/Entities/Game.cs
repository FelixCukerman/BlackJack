using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLayer.Entities
{
    [NotMapped]
    public class Game
    {
        public int Id { get; set; }
        public List<Card> Cards { get; set; }
        public List<User> Users { get; set; }
        List<Game> Games = new List<Game>();
    }
}
