using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;

namespace BlackJack.DataAccess.Repositories.DapperRepositories
{
    public class DapperGameRepository : DapperBaseRepository<Game>, IGameRepository
    {
        public DapperGameRepository(string connectionString) : base(connectionString, "Games")
        {
        }
    }
}
