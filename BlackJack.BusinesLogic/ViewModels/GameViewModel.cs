using System.Collections.Generic;

namespace BlackJack.BusinessLogic.ViewModels
{
    public class GameViewModel
    {
        public List<RoundViewModel> Rounds { get; set; }
        public GameViewModel()
        {
            Rounds = new List<RoundViewModel>();
        }
    }
}
