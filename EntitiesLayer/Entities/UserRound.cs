using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;
using EntitiesLayer.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntitiesLayer.Entities
{
    public class UserRound : BaseEntity
    {
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? RoundId { get; set; }
        [ForeignKey("RoundId")]
        public Round Round { get; set; }
        public bool? IsWin { get; set; }
        public int Rate { get; set; }

        public UserRound()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}
