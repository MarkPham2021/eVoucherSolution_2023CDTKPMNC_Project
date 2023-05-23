using eVoucher_BUS.Requests.GameRequests;
using eVoucher_BUS.Services;
using eVoucher_DTO.Models;
using eVoucher_DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVoucherDatabaseWebService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private GameService _gameService;
        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/<GameController>
        [HttpGet]
        public ActionResult<List<Game>> GetAllGames()
        {
            return Ok(_gameService.GetAllGames());
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameByID(int id)
        {
            var game = await _gameService.GetGameById(id);
            if (game == null)
            {
                return NotFound("GameID not found");
            }
            return Ok(game);
        }

        // POST api/<GameController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Game>> Post([FromBody] GameCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _gameService.AddGame(request);            
            return Ok(result);            
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
