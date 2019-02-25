using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;

namespace BlackJack.DataAccess.Repositories.EntityFrameworkRepositories
{
    public class EFGameRepository : EFBaseRepository<Game>, IGameRepository
    {
        public EFGameRepository(BlackJackContext context) : base(context)
        {
        }
    }
}
