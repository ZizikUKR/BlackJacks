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
            var allParticipantsInGame = await GetAllPlayersInCurrentGame(name, bots);
            var game = await CreateGame(allParticipantsInGame);
            Player mainPlayer = allParticipantsInGame.SingleOrDefault(p => p.PlayerRole == PlayerRole.Player);
            List<Move> moves = new List<Move>();

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
            var players = await _playerRepository.GetAll();

            List<PlayerViewModel> ListPlayersViewModel = new List<PlayerViewModel>();
            foreach (var item in players)
            {
                if (item.PlayerRole != PlayerRole.Player)
                {
                    continue;
                }

                ListPlayersViewModel.Add(new PlayerViewModel
                {
                    Id = item.Id,
                    Name = item.NickName
                });
            }
            return ListPlayersViewModel;
        }

        public async Task<GameViewModel> ShowPlayerMoves(Guid id)
        {
            var listOfMoves = (await _moveRepository.GetAllMovesForOneGame(id)).OrderBy(p => p.PlayerId);
            var allPlayers = await _playerRepository.GetAll();

            GameViewModel gameViewModel = new GameViewModel
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
            var moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
            var allPlayersExist = await _playerRepository.GetAll();

            bool isGameOver = false;
            List<Player> playersInCurrentGame = new List<Player>();


            foreach (var move in moves.Where(p => p.MoveNumber == firstMoveNumber))
            {
                playersInCurrentGame.Add(allPlayersExist.SingleOrDefault(p => p.Id == move.PlayerId));
            }

            List<PlayerViewModel> playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);

            foreach (var item in playersViews)
            {
                if (item.Points < pointsToVictory && item.PlayerRole == PlayerRole.Player)
                {
                    Player player = playersInCurrentGame.SingleOrDefault(m => m.Id == item.Id);
                    await MoveForOnePlayer(player, moves, gameId);

                    moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
                }
            }

            List<PlayerViewModel> newPlayersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
            if (newPlayersViews.SingleOrDefault(p => p.PlayerRole == PlayerRole.Player).Points >= pointsToVictory)
            {
                var result = await GetCardsForBots(gameId);
                isGameOver = result;
            }
            return isGameOver;
        }

        public async Task<bool> GetCardsForBots(Guid gameId)
        {
            var allPlayersExist = await _playerRepository.GetAll();
            var moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();

            List<Player> playersInCurrentGame = new List<Player>();
            foreach (var move in moves.Where(p => p.MoveNumber == firstMoveNumber))
            {
                playersInCurrentGame.Add(allPlayersExist.SingleOrDefault(p => p.Id == move.PlayerId));
            }

            List<PlayerViewModel> playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);

            foreach (var item in playersViews)
            {
                if (item.Points < pointsToStop)
                {
                    await GetOneMoreCardForOneBotMove(gameId, playersInCurrentGame, moves, playersViews);
                    moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
                    playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
                }
            }
            await GetCardForDealer(playersInCurrentGame, gameId, moves);

            bool isGameOver = true;
            return isGameOver;
        }

        public async Task<PlayerViewModel> GetStatusForCurrentGame(Guid id)
        {
            var playersStatusesInCurrentGame = (await _playerGameStatusRepository.GetPlayerStatusForGame(id)).ToList();
            var allPlayersExist = (await _playerRepository.GetAll()).ToList();

            GameResult mainPlayer = new GameResult();
            foreach (var item in playersStatusesInCurrentGame)
            {
                var main = allPlayersExist.SingleOrDefault(p => p.Id == item.PlayerId && p.PlayerRole == PlayerRole.Player);
                if (main != null)
                {
                    mainPlayer.GameStatus = item.GameStatus;
                }
            }

            PlayerViewModel model = new PlayerViewModel
            {
                Status = mainPlayer.GameStatus.ToString()
            };
            return model;
        }

        private async Task GetOneMoreCardForOneBotMove(Guid gameId, List<Player> playersInCurrentGame, List<Move> moves, List<PlayerViewModel> playersViews)
        {
            foreach (var item in playersViews)
            {
                if (item.Points < pointsToStop && item.PlayerRole == PlayerRole.Bot)
                {
                    Player player = playersInCurrentGame.SingleOrDefault(m => m.Id == item.Id);
                    await MoveForOnePlayer(player, moves, gameId);

                    moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
                    playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
                }
            }
        }

        private async Task GetCardForDealer(List<Player> playersInCurrentGame, Guid gameId, List<Move> moves)
        {
            List<PlayerViewModel> playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
            int counterForDealerPoints = playersViews.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer).Points;

            foreach (var item in playersViews)
            {
                if (counterForDealerPoints < pointsToStop)
                {
                    await MoveForOnePlayer(playersInCurrentGame.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer), moves, gameId);
                    moves = (await _moveRepository.GetAllMovesForOneGame(gameId)).ToList();
                    playersViews = CalculatePointsHelper.CalculatePlayersPoints(moves, playersInCurrentGame);
                    counterForDealerPoints = playersViews.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer).Points;
                }
            }
            await FinishGame(playersViews, gameId);
        }

        private async Task FinishGame(List<PlayerViewModel> playersViews, Guid gameId)
        {
            var dealer = playersViews.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer);
            playersViews.Remove(dealer);

            foreach (var item in playersViews)
            {
                if ((dealer.Points == item.Points && dealer.Points < pointsToVictory && item.Points < pointsToVictory)
                    || (dealer.Points > pointsToVictory && item.Points > pointsToVictory))
                {
                    GameResult player = new GameResult
                    {
                        GameId = gameId,
                        PlayerId = item.Id,
                        GameStatus = GameStatus.Draw
                    };
                    await AddPlayerInGameStatus(player);
                }

                if ((dealer.Points <= pointsToVictory && dealer.Points > item.Points && item.Points <= pointsToVictory)
                    || (dealer.Points <= pointsToVictory && item.Points > pointsToVictory))
                {
                    GameResult player = new GameResult
                    {
                        GameId = gameId,
                        PlayerId = item.Id,
                        GameStatus = GameStatus.Lost
                    };
                    await AddPlayerInGameStatus(player);
                }

                if ((dealer.Points <= pointsToVictory && dealer.Points < item.Points && item.Points <= pointsToVictory)
                    || (dealer.Points > pointsToVictory && item.Points <= pointsToVictory))
                {
                    GameResult player = new GameResult
                    {
                        GameId = gameId,
                        PlayerId = item.Id,
                        GameStatus = GameStatus.Won
                    };
                    await AddPlayerInGameStatus(player);
                }
            }
        }

        private async Task AddPlayerInGameStatus(GameResult player)
        {
            await _playerGameStatusRepository.Add(player);
        }

        private async Task MoveForOnePlayer(Player player, List<Move> moves, Guid gameId)
        {
            List<Card> deckOfCard = new List<Card>();

            DealCardsHelper dealDeck = new DealCardsHelper();
            dealDeck.GetAllCards(ref deckOfCard);

            DeleteUsedCardsFromDeck(ref deckOfCard, moves);

            Card cardToAdd = dealDeck.GetCard(deckOfCard);

            var moveIterator = moves.Where(p => p.PlayerId == player.Id).Count();
            Move move = new Move
            {
                CardId = cardToAdd.Id,
                GameId = gameId,
                CardName = cardToAdd.Name,
                CardPoints = cardToAdd.CardPoints,
                PlayerId = player.Id,
                MoveNumber = ++moveIterator
            };
            await _moveRepository.Add(move);
        }

        private async Task<Game> GetGame(Guid id)
        {
            Game currentGame = await _gameRepository.Get(id);
            var moves = (await _moveRepository.GetAllMovesForOneGame(id)).ToList();

            var allPlayersExist = await _playerRepository.GetAll();

            return currentGame;
        }

        private async Task<List<Player>> GetAllPlayersInCurrentGame(string name, int amoutOfBots)
        {
            var allPlayersExists = await _playerRepository.GetAll();
            var currentPlayer = await PlayerRegistration(name);

            List<Player> players = new List<Player>
            {
                currentPlayer,
                allPlayersExists.SingleOrDefault(p => p.PlayerRole == PlayerRole.Dealer)
            };

            int counterForAddingBot = 0;
            foreach (var item in allPlayersExists.Where(p => p.PlayerRole == PlayerRole.Bot))
            {
                if (item.PlayerRole == PlayerRole.Bot && counterForAddingBot < amoutOfBots)
                {
                    counterForAddingBot++;
                    players.Add(item);
                }
            }
            return players;
        }

        private async Task<Player> PlayerRegistration(string name)
        {
            var player = await _playerRepository.FindPlayerByName(name);

            if (player != null)
            {
                return player;
            }
            await _playerRepository.Add(new Player { NickName = name, PlayerRole = PlayerRole.Player });
            return await _playerRepository.FindPlayerByName(name);
        }

        private async Task<Game> CreateGame(List<Player> players)
        {
            Game game = new Game
            {
                Date = DateTime.Now,
                IsFinished = false
            };
            await _gameRepository.Add(game);
            return game;
        }

        private void Move(List<Player> players, Game game, List<Move> moves)
        {
            List<Card> deckOfCard = new List<Card>();

            DealCardsHelper dealDeck = new DealCardsHelper();
            dealDeck.GetAllCards(ref deckOfCard);



            var moveIterator = moves.Where(p => p.PlayerId == players.SingleOrDefault(x => x.PlayerRole == PlayerRole.Player).Id).Count();
            if (moveIterator > 0)
            {
                DeleteUsedCardsFromDeck(ref deckOfCard, moves);
            }

            Random random = new Random();

            foreach (var user in players)
            {
                var cardToUser = random.Next(0, deckOfCard.Count);
                Card cardToAdd = deckOfCard[cardToUser];

                moves.Add(new Move
                {
                    CardId = cardToAdd.Id,
                    CardName = cardToAdd.Name,
                    MoveNumber = moveIterator + 1,
                    GameId = game.Id,
                    PlayerId = user.Id,
                    CardPoints = cardToAdd.CardPoints
                });

                deckOfCard.Remove(cardToAdd);
            }

        }

        private void DeleteUsedCardsFromDeck(ref List<Card> deckOfCards, List<Move> moves)
        {
            foreach (var move in moves)
            {
                var card = deckOfCards.SingleOrDefault(m => m.Id == move.CardId);
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

