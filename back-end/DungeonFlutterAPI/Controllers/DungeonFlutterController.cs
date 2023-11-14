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
        private readonly IGameService gameService;

        public DungeonFlutterController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpPost("start/{rows}/{columns}")]
        public IActionResult StartGame(int rows, int columns)
        {
            World world = gameService.StartGame(rows, columns);
            var worldDTO = new WorldDTO();
            worldDTO.board = world.board;

            return Ok(worldDTO);
        }

        [HttpGet("world")]
        public IActionResult GetWorld()
        {
            World world = gameService.GetWorld();
            var worldDTO = new WorldDTO();
            worldDTO.board = world.board;

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