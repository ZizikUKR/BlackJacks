using BlackJack.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlackJack.ApiUI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
            return Json(players);
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetAllPlayerGames([FromBody] dynamic username)
        {
            string srt = username.username.ToString();

            var games = await _service.GetAllGamesForOnePlayer(srt);
            return Json(games);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllMovesForCurrentGame([FromUri] string id)
        {
            Guid gameId = Guid.Parse(id);
            var moves = await _service.GetAllMovesForCurrentGame(gameId);
            return Json(moves);
        }
    }
}
