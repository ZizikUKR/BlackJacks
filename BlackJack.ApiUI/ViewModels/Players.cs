using BlackJack.BusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlackJack.ApiUI.ViewModels
{
    public class Players
    {
        public List<PlayerViewModel> PlayerViewModels { get; set; }

        public Players()
        {
            PlayerViewModels = new List<PlayerViewModel>();
        }
    }
}