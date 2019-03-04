using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
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
            try
            {
                var playerExist = (await _service.GetAllPlayers()).ToList();
                var model = new StartGameViewModel
                {
                    Players = playerExist.Select(player => new SelectListItem
                    {
                        Value = player.Name.ToLower(),
                        Text = player.Name.ToLower()
                    }).ToList(),

                    NumberOfBots = new List<SelectListItem> {
                     new SelectListItem{ Value="1",Text="1"},
                     new SelectListItem{ Value="2",Text="2"},
                     new SelectListItem{ Value="3",Text="3"},
                     new SelectListItem{ Value="4",Text="4"}
                    }
                };
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index(string name, int bots)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return RedirectToAction("Index");
                }
                var id = await _service.StartGame(name, bots);
                return RedirectToAction("Start", "Game", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}