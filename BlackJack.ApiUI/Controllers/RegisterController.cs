using BlackJack.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlackJack.ApiUI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

            return Json(users);
        }

        [HttpPost]
        public async Task<IHttpActionResult> StartGame([FromBody] object[] mas)
        {
            var id = await _service.StartGame(Convert.ToString(mas[0]), Convert.ToInt32(mas[1]));
            return Ok(id);
        }
    }
}
