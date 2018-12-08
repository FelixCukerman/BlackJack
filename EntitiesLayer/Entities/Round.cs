using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLayer.Entities
{
    public class Round : BaseEntity
    {
        public int? GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public Round()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}
