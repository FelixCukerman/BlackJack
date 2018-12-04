using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Abstraction;
using EntitiesLayer.Entities;

namespace EntitiesLayer.Entities
{
    public class RoundStatistics : BaseEntity
    {
        public Round Round { get; set; }
        public User User { get; set; }
        public bool IsWin { get; set; }

        public RoundStatistics()
        {
            DateOfCreation = DateTime.Now;
            User user = new User();
            IsWin = false;
        }
    }
}