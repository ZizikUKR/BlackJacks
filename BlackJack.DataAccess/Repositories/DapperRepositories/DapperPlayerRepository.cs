﻿using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.DapperRepositories
{
    public class DapperPlayerRepository : DapperBaseRepository<Player>, IPlayerRepository
    {
        public DapperPlayerRepository(string connectionString) : base(connectionString, "Players")
        {

        }

        public async Task<Player> FindPlayerByName(string name)
        {
            var res = await Connection.QueryFirstOrDefaultAsync<Player>($"SELECT Id,NickName,PlayerRole FROM Players WHERE NickName=@name",new { name});
            return res;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            var res = await Connection.QueryAsync<Player>($"SELECT * FROM { _table}");
            return res;
        }
    }
}