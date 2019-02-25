using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.EntityFrameworkRepositories
{
    public class EFMoveRepository : EFBaseRepository<Move>, IMoveRepository
    {
        public EFMoveRepository(BlackJackContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Move>> GetAllMovesForOneGame(Guid id)
        {
            return await _dbContext.Set<Move>().Where(p => p.GameId == id).ToListAsync();
        }
    }
}
