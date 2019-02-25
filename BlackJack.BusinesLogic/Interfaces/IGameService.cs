using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<List<PlayerViewModel>> GetAllPlayers();
        Task<Guid> StartGame(string name, int bots);
        Task<GameViewModel> ShowPlayerMoves(Guid Id);
        Task<bool> GetOneMoreCardForPlayer(Guid gameId);
        Task<bool> GetCardsForBots(Guid gameId);
        Task<PlayerViewModel> GetStatusForCurrentGame(Guid id);
    }
}
