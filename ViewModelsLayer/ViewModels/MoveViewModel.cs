using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.ViewModels
{
    public class MoveViewModel
    {
        public UserViewModel User { get; set; }
        public bool IsWin { get; set; }
        public List<CardViewModel> Cards { get; set; }
    }
}
