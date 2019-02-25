using BlackJack.DataAccess.Entities.Enums;

namespace BlackJack.DataAccess.Entities
{
    public class Card
    {
        public long Id { get; set; }

        public CardSuit CardSuit { get; set; }

        public CardValue FaceValue { get; set; }

        public string Name { get; set; }

        public byte CardPoints { get; set; }
    }
}
