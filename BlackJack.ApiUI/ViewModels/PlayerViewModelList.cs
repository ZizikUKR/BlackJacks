using BlackJack.BusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlackJack.ApiUI.ViewModels
{
    public class PlayerViewModelList
    {
        public List<PlayerViewModel> PlayerViewModels { get; set; }

        public PlayerViewModelList()
        {
            PlayerViewModels = new List<PlayerViewModel>();
        }
    }
}