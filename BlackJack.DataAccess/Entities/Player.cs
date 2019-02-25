using BlackJack.DataAccess.Entities.Enums;

namespace BlackJack.DataAccess.Entities
{
    public class Player : BaseEntity
    {
        public string NickName { get; set; }

        public PlayerRole PlayerRole { get; set; }
    }
}
