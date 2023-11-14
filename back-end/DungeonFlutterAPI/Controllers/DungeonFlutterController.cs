using DungeonFlutterAPI.GameLogic.DTO;
using DungeonFlutterAPI.Models;
using DungeonFlutterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DungeonFlutterAPI.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class DungeonFlutterController : ControllerBase
    {
        private readonly IGame game;
        private readonly IWorldGenerator worldGenerator;

        public DungeonFlutterController(IGame game, IWorldGenerator worldGenerator)
        {
            this.game = game;
            this.worldGenerator = worldGenerator;
        }

        [HttpPost("start")]
        public IActionResult StartGame()
        {
            game.StartGame();
            return Ok("Game started successfully");
        }

        [HttpGet("world")]
        public IActionResult GetWorld()
        {
            // Assuming GetWorld method in SimpleGame to get the current world state
            World world = game.GetWorld();
            var worldDto = new WorldDTO();

            return Ok(worldDto);
        }

    }
}