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
            var playersInDB = (await _historyService.GetAllPlayers()).ToList();

            List<SelectListItem> selectListPlayers = playersInDB.Select(x => new SelectListItem
            {
                Value = x.Name.ToLower(),
                Text = x.Name.ToLower()
            }).ToList();

            ViewBag.selectListPlayers = selectListPlayers;

            return View();
        }

        public async Task<ActionResult> PlayerGames(string name)
        {
            var allGames = await _historyService.GetAllGamesForOnePlayer(name);

            return View(allGames);
        }

        public async Task<ActionResult> GameInform(Guid id)
        {
            var moves = await _historyService.GetAllMovesForCurrentGame(id);
            List<SelectListItem> selectListPlayers = new List<SelectListItem>();
            return View(moves);
        }
    }
}