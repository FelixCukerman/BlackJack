﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    public class UserGames : BaseEntity
    {
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public UserGames()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}