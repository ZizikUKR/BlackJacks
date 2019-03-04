using BlackJack.BusinessLogic.Interfaces;
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
            try
            {
                var playersExist = (await _historyService.GetAllPlayers()).ToList();
                List<SelectListItem> selectListPlayers = playersExist.Select(x => new SelectListItem
                {
                    Value = x.Name.ToLower(),
                    Text = x.Name.ToLower()
                }).ToList();

                ViewBag.selectListPlayers = selectListPlayers;

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> PlayerGames(string name)
        {            
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return RedirectToAction("ChoosePlayer");
                }
                var allGames = await _historyService.GetAllGamesForOnePlayer(name);
                return View(allGames);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> GameInform(Guid id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Error", "Home");
                }
                var moves = await _historyService.GetAllMovesForCurrentGame(id);
                return View(moves);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}