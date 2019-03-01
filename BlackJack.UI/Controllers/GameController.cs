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
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return RedirectToAction("Error","Home");
            }
            GameViewModel rounds = new GameViewModel();
            try
            {
                rounds = await _service.ShowPlayerMoves(id);
            }
            catch(Exception)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(rounds);
        }

        public async Task<ActionResult> NextMove(Guid id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return RedirectToAction("Error", "Home");
            }
            bool result = false;
            try
            {
                result = await _service.GetOneMoreCardForPlayer(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            if (result == false)
            {
                return RedirectToAction("Start", new { id });
            }
            return RedirectToAction("GameOver", new { id });
        }

        public async Task<ActionResult> Stand(Guid id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return RedirectToAction("Error", "Home");
            }
            try
            {
               var result = await _service.GetCardsForBots(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            
            return RedirectToAction("GameOver", new { id });
        }
        public async Task<ActionResult> GameOver(Guid id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return RedirectToAction("Error", "Home");
            }
            PlayerViewModel mainPlayer = new PlayerViewModel();
            try
            {
                mainPlayer = await _service.GetStatusForCurrentGame(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(mainPlayer);
        }
    }
}