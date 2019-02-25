using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IHistoryService
    {
        Task<List<PlayersViewModel>> GetAllPlayers();
        Task<List<FinishGameViewModel>> GetAllGamesForOnePlayer(string name);
        Task<List<RoundViewModel>> GetAllMovesForCurrentGame(Guid id);
    }
}
