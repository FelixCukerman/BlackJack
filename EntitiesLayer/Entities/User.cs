using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLayer.Abstraction;

namespace EntitiesLayer.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Nickname { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        [NotMapped]
        public List<Card> Cards { get; set; }
        public List<Game> Games { get; set; }

        public User()
        {
            DateOfCreation = DateTime.Now;
            Nickname = "default";
            UserRole = UserRole.None;
            Cards = new List<Card>();
        }
    }

    public enum UserRole
    {
        None = 0,
        BotPlayer = 1,
        PeoplePlayer = 2,
        Dealer = 3
    }
}
