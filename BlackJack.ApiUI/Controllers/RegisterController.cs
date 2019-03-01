using BlackJack.ApiUI.ViewModels;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.ApiUI.Controllers
{
    public class RegisterController : ApiController
    {
        private readonly IGameService _service;
        public RegisterController(IGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllUser()
        {
            List<PlayerViewModel> users = new List<PlayerViewModel>();
            try
            {
                users = await _service.GetAllPlayers();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            Players model = new Players
            {
                PlayerViewModels = users
            };
            return Ok(model);
        }

        [HttpPost]
        public async Task<IHttpActionResult> StartGame([FromBody] StartViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                return BadRequest();
            }
            Guid id = new Guid();
            try
            {
                id = await _service.StartGame(model.UserName, model.CountOfBots);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok(id);
        }
    }
}
