using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Card
    {
        public int Id { get; set; }
        public Suit Suit { get; set; }
        public string Value { get; set; }
    }
    public enum Suit
    {
        None = 0,
        Hearts = 1,
        Tiles = 2,
        Clovers = 3,
        Pikes = 4
    }
}
