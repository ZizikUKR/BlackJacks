using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<ActionResult> ChoosePlayer()
        {
            List<PlayerViewModel> playersExist = new List<PlayerViewModel>();
            try
            {
                playersExist = (await _historyService.GetAllPlayers()).ToList();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

            List<SelectListItem> selectListPlayers = playersExist.Select(x => new SelectListItem
            {
                Value = x.Name.ToLower(),
                Text = x.Name.ToLower()
            }).ToList();

            ViewBag.selectListPlayers = selectListPlayers;

            return View();
        }

        public async Task<ActionResult> PlayerGames(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
               return RedirectToAction("ChoosePlayer");
            }
            List<FinishGameViewModel> allGames = new List<FinishGameViewModel>();
            try
            {
                allGames = await _historyService.GetAllGamesForOnePlayer(name);
            }
            catch (Exception)
            {
               return RedirectToAction("Error", "Home");
            }
            return View(allGames);
        }

        public async Task<ActionResult> GameInform(Guid id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return RedirectToAction("Error", "Home");
            }
            List<RoundViewModel> moves = new List<RoundViewModel>();
            try
            {
                moves = await _historyService.GetAllMovesForCurrentGame(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
            List<SelectListItem> selectListPlayers = new List<SelectListItem>();
            return View(moves);
        }
    }
}