using System;

namespace BlackJack.DataAccess.Entities
{
    public class Game : BaseEntity
    {
        public bool IsFinished { get; set; }
        public DateTime Date { get; set; }
    }
}
