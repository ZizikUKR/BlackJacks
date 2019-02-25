using BlackJack.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlackJack.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameService _service;
        public HomeController(IGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var playersInDB = (await _service.GetAllPlayers()).ToList();

            List<SelectListItem> numberOfBots = new List<SelectListItem> {
                 new SelectListItem{ Value="1",Text="1"},
                 new SelectListItem{ Value="2",Text="2"},
                 new SelectListItem{ Value="3",Text="3"},
                 new SelectListItem{ Value="4",Text="4"}
             };

            List<SelectListItem> selectListPlayers = playersInDB.Select(x => new SelectListItem
            {
                Value = x.Name.ToLower(),
                Text = x.Name.ToLower()
            }).ToList();

            ViewBag.numberOfBots = numberOfBots;
            ViewBag.selectListPlayers = selectListPlayers;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string name, int bots)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction("Index");
            }

            var id = await _service.StartGame(name, bots);

            return RedirectToAction("Start", "Game", new { id });
        }
    }
}