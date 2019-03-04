using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
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
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Error", "Home");
                }

                var rounds = await _service.ShowPlayerMoves(id);
                return View(rounds);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> NextMove(Guid id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Error", "Home");
                }
                var result = await _service.GetOneMoreCardForPlayer(id);

                if (result == false)
                {
                    return RedirectToAction("Start", new { id });
                }
                return RedirectToAction("GameOver", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> Stand(Guid id)
        {          
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Error", "Home");
                }
                var result = await _service.GetCardsForBots(id);
                return RedirectToAction("GameOver", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }           
        }
        public async Task<ActionResult> GameOver(Guid id)
        {            
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Error", "Home");
                }
                var mainPlayer = await _service.GetStatusForCurrentGame(id);
                return View(mainPlayer);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}