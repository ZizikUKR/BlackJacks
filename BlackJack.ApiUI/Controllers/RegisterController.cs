using BlackJack.ApiUI.ViewModels;
using BlackJack.BusinessLogic.Interfaces;
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
            var users = await _service.GetAllPlayers();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IHttpActionResult> StartGame([FromBody] StartViewModel model)
        {
            var id = await _service.StartGame(model.UserName, model.CountOfBots);
            return Ok(id);
        }
    }
}
