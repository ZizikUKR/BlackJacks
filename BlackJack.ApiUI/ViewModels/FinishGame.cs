using BlackJack.BusinessLogic.ViewModels;
using System.Collections.Generic;

namespace BlackJack.ApiUI.ViewModels
{
    public class FinishGame
    {
        public List<FinishGameViewModel> FinishGameViewModels { get; set; }

        public FinishGame()
        {
            FinishGameViewModels = new List<FinishGameViewModel>();
        }
    }
}