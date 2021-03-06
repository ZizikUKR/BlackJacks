﻿using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Entities.Enums;
using BlackJack.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.EntityFrameworkRepositories
{
    public class EFPlayerRepository : EFBaseRepository<Player>, IPlayerRepository
    {
        public EFPlayerRepository(BlackJackContext contex)
            : base(contex)
        {
        }

        public async Task<Player> FindPlayerByName(string name)
        {
            return await _dbContext.Set<Player>().FirstOrDefaultAsync(m => m.NickName == name);
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await _dbContext.Set<Player>().ToListAsync();
        }
        public async Task<IEnumerable<Player>> GetAllBotsAndDealer()
        {
            return await _dbContext.Set<Player>().Where(p => p.PlayerRole==PlayerRole.Dealer || p.PlayerRole==PlayerRole.Bot).ToListAsync();            
        }
    }
}
