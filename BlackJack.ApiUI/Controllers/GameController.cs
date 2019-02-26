using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
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
        public async Task<IHttpActionResult> GetFirsTwoMoves([FromUri] string id)
        {
            var moves = await _service.ShowPlayerMoves(Guid.Parse(id));

            List<RoundViewModel> list = moves.Rounds.Select(p => new RoundViewModel
            {
                Id = p.Id,
                CardValue = p.CardValue,
                GameId = p.GameId,
                PlayerNickName = p.PlayerNickName,
                RoundNumber = p.RoundNumber
            }).ToList();
            return Ok(list);
        }

        [HttpGet]
        public async Task<IHttpActionResult> NextRoundForPlayer([FromUri] string id)
        {
            Guid gameId = Guid.Parse(id);
            var isGameOver = await _service.GetOneMoreCardForPlayer(gameId);


            return Ok(isGameOver);
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
