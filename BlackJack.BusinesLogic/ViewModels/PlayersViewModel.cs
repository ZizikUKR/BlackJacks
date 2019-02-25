using BlackJack.DataAccess.Entities.Enums;
using System;

namespace BlackJack.BusinessLogic.ViewModels
{
    public class PlayersViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public PlayerRole PlayerRole { get; set; }

        public string Status { get; set; }
    }
}
