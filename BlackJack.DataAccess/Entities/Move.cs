using System;

namespace BlackJack.DataAccess.Entities
{
    public class Move : BaseEntity
    {
        public long MoveNumber { get; set; }

        public Guid PlayerId { get; set; }

        public long CardId { get; set; }
        public string CardName { get; set; }
        public byte CardPoints { get; set; }

        public Guid GameId { get; set; }
    }
}
