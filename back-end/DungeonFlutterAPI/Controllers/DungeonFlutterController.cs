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
        private readonly IGame game;
        private readonly IWorldGenerator worldGenerator;
        private readonly IGameService gameService;

        public DungeonFlutterController(IGame game, IWorldGenerator worldGenerator, IGameService gameService)
        {
            this.game = game;
            this.worldGenerator = worldGenerator;
            this.gameService = gameService;
        }

        [HttpPost("start")]
        public IActionResult StartGame()
        {
            gameService.StartGame();
            return Ok("Game started successfully");
        }

        [HttpGet("world")]
        public IActionResult GetWorld()
        {
            World world = gameService.GetWorld();
            var worldDTO = new WorldDTO();
            worldDTO.Tiles = world.Tiles;

            return Ok(worldDTO);
        }

        [HttpGet("highscore/{playerName}")]
        public IActionResult GetHighScore(string playerName)
        {
            var highScore = gameService.GetHighScore(playerName);

            if (highScore != null)
            {
                return Ok(new { player = highScore.Player, highscore = highScore.HighScore });
            }

            return NotFound($"High score not found for player: {playerName}");
        }

        [HttpPost("savehighscore/{playerName}")]
        public IActionResult SaveHighScore(string playerName, int highscore)
        {
            gameService.SaveHighScore(playerName, highscore);

            return Ok(new { player = playerName, highscore });
        }

    }
}