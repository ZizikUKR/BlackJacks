using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.EntityFrameworkRepositories
{
    public class EFGameResultsRepository : EFBaseRepository<GameResult>, IPlayerGameStatusRepository
    {
        public EFGameResultsRepository(BlackJackContext context) : base(context)
        {
        }
        public async Task<IEnumerable<GameResult>> GetPlayerStatusForGame(Guid gameId)
        {
            return await _dbContext.Set<GameResult>().Where(p => p.GameId == gameId).ToListAsync();
        }

        public async Task<IEnumerable<GameResult>> GetGameResultForOnePlayer(Guid playerId)
        {
            return await _dbContext.Set<GameResult>().Where(p => p.PlayerId == playerId).ToListAsync();
        }
    }
}
