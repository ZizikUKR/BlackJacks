﻿using BlackJack.ApiUI.ViewModels;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.ApiUI.Controllers
{
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _service;
        public HistoryController(IHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPlayers()
        {
            try
            {
                List<PlayerViewModel> players = await _service.GetAllPlayers();
                var model = new Players
                {
                    PlayerViewModels = players
                };
                return Ok(model);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetAllPlayerGames([FromBody] PlayerViewModel body)
        {            
            try
            {
                if (string.IsNullOrWhiteSpace(body.Name))
                {
                    return BadRequest();
                }
                List<FinishGameViewModel> games = await _service.GetAllGamesForOnePlayer(body.Name);

                var model = new FinishGame
                {
                    FinishGameViewModels = games
                };
                return Ok(model);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllMovesForCurrentGame([FromUri] string id)
        {           
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }
                List<RoundViewModel> moves = await _service.GetAllMovesForCurrentGame(Guid.Parse(id));
                var model = new Rounds
                {
                    RoundViewModels = moves
                };
                return Ok(model);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
