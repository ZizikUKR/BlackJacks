using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using BlackJack.DataAccess.Entities.Enums;
using BlackJack.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class HistoryService : IHistoryService
    {
        private IPlayerRepository _playerRepository;
        private IMoveRepository _moveRepository;
        private IPlayerGameStatusRepository _playerGameStatusRepository;

        public HistoryService(IPlayerRepository playerRepository, IMoveRepository moveRepository, IPlayerGameStatusRepository playerGameStatusRepository)
        {
            _playerRepository = playerRepository;
            _moveRepository = moveRepository;
            _playerGameStatusRepository = playerGameStatusRepository;
        }

        public async Task<List<PlayerViewModel>> GetAllPlayers()
        {
            var players = await _playerRepository.GetAll();

            var playersViewModel = new List<PlayerViewModel>();
            foreach (var item in players)
            {
                if (item.PlayerRole == PlayerRole.Player)
                {
                    PlayerViewModel player = new PlayerViewModel
                    {
                        Id = item.Id,
                        Name = item.NickName
                    };
                    playersViewModel.Add(player);
                }
            }
            return playersViewModel;
        }

        public async Task<List<FinishGameViewModel>> GetAllGamesForOnePlayer(string name)
        {
            var playersExist = (await _playerRepository.GetAll()).ToList();
            var mainPlayer = playersExist.FirstOrDefault(p => p.NickName == name);

            var allGamesForCurrenPlayer = (await _playerGameStatusRepository.GetGameResultForOnePlayer(mainPlayer.Id)).ToList();

            var gamesResults = new List<FinishGameViewModel>();
            foreach (var item in allGamesForCurrenPlayer)
            {
                gamesResults.Add(new FinishGameViewModel
                {
                    Id = item.GameId,
                    Player = mainPlayer.NickName,
                    Result = item.GameStatus.ToString(),
                    IsFinished =true
                });
            }
            return gamesResults;
        }

        public async Task<List<RoundViewModel>> GetAllMovesForCurrentGame(Guid id)
        {
            var moves= (await _moveRepository.GetAllMovesForOneGame(id)).ToList();
            var allPlayersExist = (await _playerRepository.GetAll()).ToList();
            var rounds = new List<RoundViewModel>();

            foreach (var item in moves)
            {
                rounds.Add(new RoundViewModel
                {
                    CardValue=item.CardName,
                    Id= item.Id,
                    GameId=item.GameId,
                    PlayerNickName = allPlayersExist.FirstOrDefault(p=>p.Id==item.PlayerId).NickName.ToString(),
                    RoundNumber = item.MoveNumber
                });
            }
            return rounds;
        }
    }
}
