using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.ViewModels
{
    public class RoundViewModel
    {
        public List<MoveViewModel> Moves { get; set; }
        public bool IsOver { get; set; }
    }
}
