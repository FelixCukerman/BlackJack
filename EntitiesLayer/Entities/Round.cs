using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace EntitiesLayer.Entities
{
    public class Round : BaseEntity
    {
        public Game Game { get; set; }
        public List<Move> Moves { get; set; }
        public List<RoundStatistics> RoundStatistics { get; set; }

        public Round()
        {
            DateOfCreation = DateTime.Now;
            Moves = new List<Move>();
        }
    }
}
