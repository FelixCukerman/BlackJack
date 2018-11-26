using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntitiesLayer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        public List<Card> DiscardPile { get; set; }

        public User()
        {
            DateOfCreation = DateTime.Now;
            Nickname = "default";
            UserRole = UserRole.None;
            DiscardPile = new List<Card>();
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
