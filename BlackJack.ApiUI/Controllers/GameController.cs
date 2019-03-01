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
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            GameViewModel moves = new GameViewModel();
            try
            {
                moves = await _service.ShowPlayerMoves(Guid.Parse(id));
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            var list = moves.Rounds.Select(p => new RoundViewModel
            {
                Id = p.Id,
                CardValue = p.CardValue,
                GameId = p.GameId,
                PlayerNickName = p.PlayerNickName,
                RoundNumber = p.RoundNumber
            }).ToList();

            Rounds model = new Rounds
            {
                RoundViewModels = list
            };
            return Ok(model);
        }

        [HttpGet]
        public async Task<IHttpActionResult> NextRoundForPlayer([FromUri] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            Guid gameId = Guid.Parse(id);

            bool isGameOver = false;
            try
            {
                isGameOver = await _service.GetOneMoreCardForPlayer(gameId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(isGameOver);
        }

        [HttpGet]
        public async Task<IHttpActionResult> DealRestOfCards([FromUri] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            Guid gameId = Guid.Parse(id);
            bool res = false;
            try
            {
                res = await _service.GetCardsForBots(gameId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GameResult([FromUri] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            Guid gameId = Guid.Parse(id);
            PlayerViewModel mainPlayer = new PlayerViewModel();
            try
            {
                mainPlayer = await _service.GetStatusForCurrentGame(gameId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(mainPlayer);
        }

    }
}
