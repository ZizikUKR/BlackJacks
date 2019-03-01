using BlackJack.BusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlackJack.ApiUI.ViewModels
{
    public class RoundsViewModelList
    {
        public List<RoundViewModel> RoundViewModels { get; set; }

        public RoundsViewModelList()
        {
            RoundViewModels = new List<RoundViewModel>();
        }
    }
}