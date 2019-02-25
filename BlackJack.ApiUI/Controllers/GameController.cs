using BlackJack.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.ApiUI.Controllers
{
    public class GameController : ApiController
    {
        private readonly IGameService _service;
        public GameController(IGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetFirsTwoMoves([FromUri] string id)
        {
            var moves = await _service.ShowPlayerMoves(Guid.Parse(id));
            return Ok(moves);
        }

        [HttpGet]
        public async Task<IHttpActionResult> NextRoundForPlayer([FromUri] string id)
        {
            Guid gameId = Guid.Parse(id);
            var isGameOver = await _service.GetOneMoreCardForPlayer(gameId);

            var moves = await _service.ShowPlayerMoves(gameId);
         
            return Ok(moves);
        }

        [HttpGet]
        public async Task<IHttpActionResult> DealRestOfCards([FromUri] string id)
        {
            Guid gameId = Guid.Parse(id);

            var res = await _service.GetCardsForBots(gameId);

            return Ok(res);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GameResult([FromUri] string id)
        {
            Guid gameId = Guid.Parse(id);

            var mainPlayer = await _service.GetStatusForCurrentGame(gameId);

            return Ok(mainPlayer);
        }

    }
}
