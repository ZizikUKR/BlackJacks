using System;

namespace BlackJack.BusinessLogic.ViewModels
{
    public class RoundViewModel
    {
        public Guid Id { get; set; }

        public string PlayerNickName { get; set; }

        public string CardValue { get; set; }

        public long RoundNumber { get; set; }

        public Guid GameId { get; set; }
    }
}
