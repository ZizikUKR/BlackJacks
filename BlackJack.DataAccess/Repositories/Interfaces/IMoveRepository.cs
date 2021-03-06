﻿using BlackJack.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IMoveRepository : IBaseRepository<Move>
    {
        Task<IEnumerable<Move>> GetAllMovesForOneGame(Guid id);
    }
}
