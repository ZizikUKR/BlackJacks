using BlackJack.BusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlackJack.ApiUI.ViewModels
{
    public class FinishGameViewModelList
    {
        public List<FinishGameViewModel> FinishGameViewModels { get; set; }

        public FinishGameViewModelList()
        {
            FinishGameViewModels = new List<FinishGameViewModel>();
        }
    }
}