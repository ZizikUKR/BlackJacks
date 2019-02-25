using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.DapperRepositories
{
    public class DapperMoveRepository : DapperBaseRepository<Move>, IMoveRepository
    {
        public DapperMoveRepository(string connectionString) : base(connectionString, "Moves")
        {
        }

        public async Task<IEnumerable<Move>> GetAllMovesForOneGame(Guid id)
        {
            return await Connection.QueryAsync<Move>("SELECT * FROM Moves WHERE GameId=@Id",new { Id=id});
        }
    }
}
