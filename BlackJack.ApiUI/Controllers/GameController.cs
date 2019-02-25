using BlackJack.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlackJack.ApiUI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
            return Json(moves);
        }

        [HttpGet]
        public async Task<IHttpActionResult> NextRoundForPlayer([FromUri] string id)
        {
            Guid gameId = Guid.Parse(id);
            var isGameOver = await _service.GetOneMoreCardForPlayer(gameId);

            var moves = await _service.ShowPlayerMoves(gameId);

            if (isGameOver == false)
            {
                return Json(moves);
            }
            return Json(moves);
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

            return Json(mainPlayer);
        }

    }
}
