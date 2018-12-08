﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EntitiesLayer.Entities
{
    public class Move : BaseEntity
    {
        public bool? IsWin { get; set; }
        public int? RoundId { get; set; }
        [ForeignKey("RoundId")]
        public Round Round { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        public int CurrentRate { get; set; }

        public Move()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}