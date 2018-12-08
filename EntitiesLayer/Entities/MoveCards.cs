using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    public class MoveCards : BaseEntity
    {
        public int? MoveId { get; set; }
        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        public int CardId { get; set; }
        [ForeignKey("CardId")]
        public Card Card { get; set; }

        public MoveCards()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}
