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
            try
            {
                List<PlayerViewModel> users = await _service.GetAllPlayers();
                Players model = new Players
                {
                    PlayerViewModels = users
                };
                return Ok(model);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> StartGame([FromBody] StartViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                {
                    return BadRequest();
                }
                Guid id = await _service.StartGame(model.UserName, model.CountOfBots);
                return Ok(id);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
