using BlackJack.ApiUI.ViewModels;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.ApiUI.Controllers
{
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _service;
        public HistoryController(IHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPlayers()
        {
            var players = await _service.GetAllPlayers();
            PlayerViewModelList model = new PlayerViewModelList
            {
                PlayerViewModels = players
            };
            return Ok(model);
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetAllPlayerGames([FromBody] PlayerViewModel body)
        {
            var games = await _service.GetAllGamesForOnePlayer(body.Name);
            FinishGameViewModelList model = new FinishGameViewModelList
            {
                FinishGameViewModels = games
            };
            return Ok(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllMovesForCurrentGame([FromUri] string id)
        {
            Guid gameId = Guid.Parse(id);
            var moves = await _service.GetAllMovesForCurrentGame(gameId);
            return Ok(moves);
        }
    }
}
