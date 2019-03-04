using BlackJack.BusinessLogic.Helpers;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using BlackJack.DataAccess.Entities;
using BlackJack.DataAccess.Entities.Enums;
using BlackJack.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private IPlayerRepository _playerRepository;
        private IGameRepository _gameRepository;
        private IMoveRepository _moveRepository;
        private IPlayerGameStatusRepository _playerGameStatusRepository;

        private const int pointsToVictory = 21;
        private const int pointsToStop = 17;
        private const int firstMoveNumber = 1;

        public GameService(IPlayerRepository playerRepository, IGameRepository gameRepository, IMoveRepository moveRepository, IPlayerGameStatusRepository playerGameStatusRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _moveRepository = moveRepository;
            _playerGameStatusRepository = playerGameStatusRepository;
        }

        public async Task<Guid> StartGame(string name, int bots)
        {
            List<Player> allParticipantsInGame = await GetAllPlayersInCurrentGame(name, bots);
            Game game = await CreateGame(allParticipantsInGame);
            Player mainPlayer = allParticipantsInGame.SingleOrDefault(p => p.PlayerRole == PlayerRole.Player);
            var moves = new List<Move>();

            int firstTwoMovesIterator = 2;
            for (int i = 1; i <= firstTwoMovesIterator; i++)
            {
                Move(allParticipantsInGame, game, moves);
                await AddFirstTwoMoves(moves, i);
            }
            return game.Id;
        }

        public async Task<List<PlayerViewModel>> GetAllPlayers()
        {
            IEnumerable<Player> players = await _playerRepository.GetAll();

            var playersViewModel = new List<PlayerViewModel>();
            foreach (var item in players)
            {
                if (item.PlayerRole != PlayerRole.Player)
                {
                    continue;
                }
                playersViewModel.Add(new PlayerViewModel
                {
                    Id = item.Id,
                    Name = item.NickName
                });
            }
            return playersViewModel;
        }

        public async Task<GameViewModel> ShowPlayerMoves(Guid id)
        {
            IOrderedEnumerable<Move> listOfMoves = (await _moveRepository.GetAllMovesForOneGame(id)).OrderBy(p => p.PlayerId);
            var allPlayers = await _playerRepository.GetAll();

            var gameViewModel = new GameViewModel
            {
                Rounds = listOfMoves.Select(p => new RoundViewModel
                {
                    Id = p.Id,
                    CardValue = p.CardName,
                    GameId = p.GameId,
                    RoundNumber = p.MoveNumber,
                    PlayerNickName = allPlayers.SingleOrDefault(m => m.Id == p.PlayerId).NickName
                }).ToList()
            };
            return gameViewModel;
        }

        public async Task<bool> GetOneMoreCardForPlayer(Guid gameId)
        {
            Game currentGame = await GetGame(gameId);
            List<Move> moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
            IEnumerable<Player> allPlayersExist = await _playerRepository.GetAll();

            bool isGameOver = false;
            var playersInCurrentGame = new List<Player>();


            foreach (var move in moves.Where(p => p.MoveNumber == firstMoveNumber))
            {
                playersInCurrentGame.Add(allPlayersExist.SingleOrDefault(p => p.Id == move.PlayerId));
            }

            List<PlayerViewModel> playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);

            foreach (var item in playersViews)
            {
                if (item.Points < pointsToVictory && item.PlayerRole != PlayerRole.Player)
                {
                    continue;
                }
                Player player = playersInCurrentGame.SingleOrDefault(m => m.Id == item.Id);
                await MoveForOnePlayer(player, moves, gameId);

                moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
            }

            List<PlayerViewModel> newPlayersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
            if (newPlayersViews.FirstOrDefault(p => p.PlayerRole == PlayerRole.Player)?.Points >= pointsToVictory)
            {
                var result = await GetCardsForBots(gameId);
                isGameOver = result;
            }
            return isGameOver;
        }

        public async Task<bool> GetCardsForBots(Guid gameId)
        {
            IEnumerable<Player> allPlayersExist = await _playerRepository.GetAll();
            List<Move> moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();

            var playersInCurrentGame = new List<Player>();
            foreach (var move in moves.Where(p => p.MoveNumber == firstMoveNumber))
            {
                playersInCurrentGame.Add(allPlayersExist.SingleOrDefault(p => p.Id == move.PlayerId));
            }

            List<PlayerViewModel> playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);

            foreach (var item in playersViews)
            {
                if (item.Points >= pointsToStop)
                {
                    continue;
                }
                await GetOneMoreCardForOneBotMove(gameId, playersInCurrentGame, moves, playersViews);
                moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
                playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
            }
            await GetCardForDealer(playersInCurrentGame, gameId, moves);

            bool isGameOver = true;
            return isGameOver;
        }

        public async Task<PlayerViewModel> GetStatusForCurrentGame(Guid id)
        {
            List<GameResult> playersStatusesInCurrentGame = (await _playerGameStatusRepository.GetPlayerStatusForGame(id)).ToList();
            List<Player> allPlayersExist = (await _playerRepository.GetAll()).ToList();

            var mainPlayer = new GameResult();
            foreach (var item in playersStatusesInCurrentGame)
            {
                var main = allPlayersExist.SingleOrDefault(p => p.Id == item.PlayerId && p.PlayerRole == PlayerRole.Player);
                if (main == null)
                {
                    continue;
                }
                mainPlayer.GameStatus = item.GameStatus;
            }

            var model = new PlayerViewModel
            {
                Status = mainPlayer.GameStatus.ToString()
            };
            return model;
        }

        private async Task GetOneMoreCardForOneBotMove(Guid gameId, List<Player> playersInCurrentGame, List<Move> moves, List<PlayerViewModel> playersViews)
        {
            foreach (var item in playersViews.Where(p => p.PlayerRole == PlayerRole.Bot))
            {
                if (item.Points >= pointsToStop)
                {
                    continue;
                }
                Player player = playersInCurrentGame.SingleOrDefault(m => m.Id == item.Id);
                await MoveForOnePlayer(player, moves, gameId);

                moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
                playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
            }
        }

        private async Task GetCardForDealer(List<Player> playersInCurrentGame, Guid gameId, List<Move> moves)
        {
            List<PlayerViewModel> playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
            int counterForDealerPoints = playersViews.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer).Points;

            foreach (var item in playersViews)
            {
                if (counterForDealerPoints >= pointsToStop)
                {
                    continue;
                }
                await MoveForOnePlayer(playersInCurrentGame.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer), moves, gameId);
                moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
                playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
                counterForDealerPoints = playersViews.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer).Points;
            }
            await FinishGame(playersViews, gameId);
        }

        private async Task FinishGame(List<PlayerViewModel> playersViews, Guid gameId)
        {
            PlayerViewModel dealer = playersViews.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer);
            playersViews.Remove(dealer);

            foreach (var item in playersViews)
            {
                if ((dealer.Points == item.Points && dealer.Points < pointsToVictory && item.Points < pointsToVictory)
                    || (dealer.Points > pointsToVictory && item.Points > pointsToVictory))
                {
                    await AddPlayerInGameStatus(item, gameId, GameStatus.Draw);
                }

                if ((dealer.Points <= pointsToVictory && dealer.Points > item.Points && item.Points <= pointsToVictory)
                    || (dealer.Points <= pointsToVictory && item.Points > pointsToVictory))
                {
                    await AddPlayerInGameStatus(item, gameId, GameStatus.Lost);
                }

                if ((dealer.Points <= pointsToVictory && dealer.Points < item.Points && item.Points <= pointsToVictory)
                    || (dealer.Points > pointsToVictory && item.Points <= pointsToVictory))
                {
                    await AddPlayerInGameStatus(item, gameId, GameStatus.Won);
                }
            }
        }

        private async Task AddPlayerInGameStatus(PlayerViewModel item, Guid gameId, GameStatus gameStatus)
        {
            var player = new GameResult
            {
                GameId = gameId,
                PlayerId = item.Id,
                GameStatus = gameStatus
            };
            await _playerGameStatusRepository.Add(player);
        }

        private async Task MoveForOnePlayer(Player player, List<Move> moves, Guid gameId)
        {
            var deckOfCard = new List<Card>();

            var dealDeck = new DealCardsHelper();
            dealDeck.GetAllCards(ref deckOfCard);

            DeleteUsedCardsFromDeck(deckOfCard, moves);

            Card cardToAdd = dealDeck.GetCard(deckOfCard);

            int moveIterator = moves.Where(p => p.PlayerId == player.Id).Count();
            var move = new Move
            {
                CardId = cardToAdd.Id,
                GameId = gameId,
                CardName = cardToAdd.Name,
                CardPoints = cardToAdd.CardPoints,
                PlayerId = player.Id,
                MoveNumber = moveIterator+1
            };
            await _moveRepository.Add(move);
        }

        private async Task<Game> GetGame(Guid id)
        {
            Game currentGame = await _gameRepository.Get(id);
            return currentGame;
        }

        private async Task<List<Player>> GetAllPlayersInCurrentGame(string name, int amoutOfBots)
        {
            IEnumerable<Player> allPlayersExists = await _playerRepository.GetAllBotsAndDealer();
            Player currentPlayer = await PlayerRegistration(name);

            var players = new List<Player>
            {
                currentPlayer,
                allPlayersExists.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer)
            };

            int counterForAddingBot = 0;
            foreach (var item in allPlayersExists.Where(p => p.PlayerRole == PlayerRole.Bot))
            {
                if (counterForAddingBot >= amoutOfBots)
                {
                    continue;
                }
                counterForAddingBot++;
                players.Add(item);
            }
            return players;
        }

        private async Task<Player> PlayerRegistration(string name)
        {
            Player player = await _playerRepository.FindPlayerByName(name);

            if (player != null)
            {
                return player;
            }
            var newPlayer = new Player { NickName = name, PlayerRole = PlayerRole.Player };
            await _playerRepository.Add(newPlayer);

            return newPlayer;
        }

        private async Task<Game> CreateGame(List<Player> players)
        {
            var game = new Game
            {
                Date = DateTime.Now,
                IsFinished = false
            };
            await _gameRepository.Add(game);
            return game;
        }

        private void Move(List<Player> players, Game game, List<Move> moves)
        {
            var deckOfCard = new List<Card>();

            var dealDeck = new DealCardsHelper();
            dealDeck.GetAllCards(ref deckOfCard);

            int moveIterator = moves.Where(p => p.PlayerId == players.SingleOrDefault(x => x.PlayerRole == PlayerRole.Player)?.Id).Count();
            if (moveIterator > 0)
            {
                DeleteUsedCardsFromDeck(deckOfCard, moves);
            }

            var random = new Random();

            foreach (var user in players)
            {
                int cardToUser = random.Next(0, deckOfCard.Count);
                Card cardToAdd = deckOfCard[cardToUser];
                moves.Add(new Move
                {
                    CardId = cardToAdd.Id,
                    CardName = cardToAdd.Name,
                    MoveNumber = moveIterator+1,
                    GameId = game.Id,
                    PlayerId = user.Id,
                    CardPoints = cardToAdd.CardPoints
                });
                deckOfCard.Remove(cardToAdd);                
            }
        }

        private void DeleteUsedCardsFromDeck(List<Card> deckOfCards, List<Move> moves)
        {
            foreach (var move in moves)
            {
                Card card = deckOfCards.SingleOrDefault(m => m.Id == move.CardId);
                deckOfCards.Remove(card);
            }
        }

        private async Task AddFirstTwoMoves(IEnumerable<Move> moves, long moveNumber)
        {
            foreach (var item in moves)
            {
                if (item.MoveNumber == moveNumber)
                {
                    await _moveRepository.Add(item);
                }
            }
        }
    }
}

