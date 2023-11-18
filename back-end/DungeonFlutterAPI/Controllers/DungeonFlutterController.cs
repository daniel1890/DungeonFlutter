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

        [HttpGet("highscore/{playerId}")]
        public IActionResult GetHighScore(int playerId)
        {
            var highScore = gameService.GetHighScore(playerId);

            if (highScore != null)
            {
                return Ok(new { player = highScore.Player, highscore = highScore.HighScore });
            }

            return NotFound($"High score not found for player with ID: {playerId}");
        }

        [HttpPost("savehighscore/{playerId}")]
        public IActionResult SaveHighScore(int playerId, int highscore)
        {
            gameService.SaveHighScore(playerId, highscore);

            return Ok(new { player = playerId, highscore });
        }

    }
}