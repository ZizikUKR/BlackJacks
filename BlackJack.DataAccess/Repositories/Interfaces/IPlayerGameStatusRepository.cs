using BlackJack.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IPlayerGameStatusRepository : IBaseRepository<GameResult>
    {
        Task<IEnumerable<GameResult>> GetPlayerStatusForGame(Guid id);
        Task<IEnumerable<GameResult>> GetGameResultForOnePlayer(Guid playerId);
    }
}
