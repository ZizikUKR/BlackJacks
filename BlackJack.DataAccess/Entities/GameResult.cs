using BlackJack.DataAccess.Entities.Enums;
using System;

namespace BlackJack.DataAccess.Entities
{
    public class GameResult : BaseEntity
    {
        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }

        public GameStatus GameStatus { get; set; }

    }
}
