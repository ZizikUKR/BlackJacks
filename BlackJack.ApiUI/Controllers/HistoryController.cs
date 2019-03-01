using BlackJack.ApiUI.ViewModels;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
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
            List<PlayerViewModel> players = new List<PlayerViewModel>();
            try
            {
                 players = await _service.GetAllPlayers();
            }
            catch(Exception)
            {
                return InternalServerError();
            }

            Players model = new Players
            {
                PlayerViewModels = players
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetAllPlayerGames([FromBody] PlayerViewModel body)
        {
            if (string.IsNullOrWhiteSpace(body.Name))
            {
                return BadRequest();
            }
            List<FinishGameViewModel> games = new List<FinishGameViewModel>();
            try
            {
                 games = await _service.GetAllGamesForOnePlayer(body.Name);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
           
            FinishGame model = new FinishGame
            {
                FinishGameViewModels = games
            };
            return Ok(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllMovesForCurrentGame([FromUri] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            Guid gameId = Guid.Parse(id);
            List<RoundViewModel> moves = new List<RoundViewModel>();
            try
            {
                moves = await _service.GetAllMovesForCurrentGame(gameId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
                        
            Rounds model = new Rounds
            {
                RoundViewModels = moves
            };
            return Ok(model);
        }
    }
}
