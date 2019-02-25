using System.Collections.Generic;
using System.Web.Mvc;

namespace BlackJack.BusinessLogic.ViewModels
{
    public class StartGameViewModel
    {
        public List<SelectListItem> Players { get; set; }

        public List<SelectListItem> NumberOfBots { get; set; }

        public StartGameViewModel()
        {
            Players = new List<SelectListItem>();
            NumberOfBots = new List<SelectListItem>();
        }
    }
}
