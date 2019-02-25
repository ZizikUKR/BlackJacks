using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.DapperRepositories
{
    public class DapperGameResultRepository : DapperBaseRepository<GameResult>, IPlayerGameStatusRepository
    {
        public DapperGameResultRepository(string connectionString) : base(connectionString, "GameResults")
        {
        }

        public async Task<IEnumerable<GameResult>> GetPlayerStatusForGame(Guid id)
        {
            return await Connection.QueryAsync<GameResult>("SELECT * FROM GameResults WHERE GameId=@Id", new { Id = id });
        }


        public async Task<IEnumerable<GameResult>> GetGameResultForOnePlayer(Guid playerId)
        {
            return await Connection.QueryAsync<GameResult>("SELECT * FROM GameResults WHERE PlayerId=@Id", new { Id = playerId });
        }
    }
}
