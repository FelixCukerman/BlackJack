﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    public class Game : BaseEntity
    {
        [Required]
        public int RoundQuantity { get; set; }

        public Game()
        {
            DateOfCreation = DateTime.Now;
            RoundQuantity = 3;
        }
    }
}