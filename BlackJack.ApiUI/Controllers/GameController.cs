using BlackJack.ApiUI.ViewModels;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Linq;
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
        public async Task<IHttpActionResult> GetFirstTwoMoves([FromUri] string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }
                GameViewModel moves = await _service.ShowPlayerMoves(Guid.Parse(id));
                var list = moves.Rounds.Select(p => new RoundViewModel
                {
                    Id = p.Id,
                    CardValue = p.CardValue,
                    GameId = p.GameId,
                    PlayerNickName = p.PlayerNickName,
                    RoundNumber = p.RoundNumber
                }).ToList();

                var model = new Rounds
                {
                    RoundViewModels = list
                };
                return Ok(model);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> NextRoundForPlayer([FromUri] string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }
                bool isGameOver = await _service.GetOneMoreCardForPlayer(Guid.Parse(id));
                return Ok(isGameOver);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> DealRestOfCards([FromUri] string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }
                bool res = await _service.GetCardsForBots(Guid.Parse(id));
                return Ok(res);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GameResult([FromUri] string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }
                PlayerViewModel mainPlayer = await _service.GetStatusForCurrentGame(Guid.Parse(id));
                return Ok(mainPlayer);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
