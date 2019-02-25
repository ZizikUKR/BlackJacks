using BlackJack.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _service;
        public GameController(IGameService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Start(Guid id)
        {
            var rounds = await _service.ShowPlayerMoves(id);

            return View(rounds);
        }

        public async Task<ActionResult> NextMove(Guid id)
        {
            var result = await _service.GetOneMoreCardForPlayer(id);
            if (result == false)
            {
                return RedirectToAction("Start", new { id });
            }
            return RedirectToAction("GameOver", new { id });
        }

        public async Task<ActionResult> Stand(Guid id)
        {
            var res = await _service.GetCardsForBots(id);

            return RedirectToAction("GameOver", new { id });
        }
        public async Task<ActionResult> GameOver(Guid id)
        {
            var mainPlayer = await _service.GetStatusForCurrentGame(id);
            return View(mainPlayer);
        }
    }
}