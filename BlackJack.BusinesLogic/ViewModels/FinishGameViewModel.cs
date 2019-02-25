using System;

namespace BlackJack.BusinessLogic.ViewModels
{
    public class FinishGameViewModel
    {
        public Guid Id { get; set; }

        public string Player { get; set; }

        public bool IsFinished { get; set; }

        public string Result { get; set; }

    }
}
