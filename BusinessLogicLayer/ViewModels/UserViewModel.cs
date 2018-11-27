using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Entities;

namespace BusinessLogicLayer.ViewModels
{
    public class UserViewModel
    {
        public string Nickname { get; set; }
        public UserRole UserRole { get; set; }
        public List<Card> Cards { get; set; }
    }
}
