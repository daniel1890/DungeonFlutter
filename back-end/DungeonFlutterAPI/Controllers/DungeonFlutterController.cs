using DungeonFlutterAPI.GameLogic.DTO;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Services.Implementations;
using DungeonFlutterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DungeonFlutterAPI.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class DungeonFlutterController : ControllerBase
    {
        private readonly IGameService _gameService;

        public DungeonFlutterController(IGameService gameService)
        {
            this._gameService = gameService;
        }

        [HttpPost("start/{rows}/{columns}")]
        public IActionResult StartGame(int rows, int columns)
        {
            World world = _gameService.StartGame(rows, columns);
            var worldDTO = new WorldDTO();
            worldDTO.board = world.board;

            return Ok(worldDTO);
        }

    }
}