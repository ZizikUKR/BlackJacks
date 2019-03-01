using BlackJack.BusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlackJack.ApiUI.ViewModels
{
    public class Rounds
    {
        public List<RoundViewModel> RoundViewModels { get; set; }

        public Rounds()
        {
            RoundViewModels = new List<RoundViewModel>();
        }
    }
}