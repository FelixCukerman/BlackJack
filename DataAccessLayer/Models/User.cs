using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        public UserRole UserRole { get; set; }
    }

    public enum UserRole
    {
        None = 0,
        BotPlayer = 1,
        PeoplePlayer = 2,
        Dealer = 3
    }
}