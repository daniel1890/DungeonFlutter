using DungeonFlutterAPI.Services.Implementations;
using DungeonFlutterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DungeonFlutterAPI.Controllers
{
    [ApiController]
    [Route("api/highscore")]
    public class HigScoreController : ControllerBase
    {
        private readonly IHighScoreService _highScoreService;

        public HigScoreController(IHighScoreService highScoreService)
        {
            this._highScoreService = highScoreService;
        }

        [HttpGet("highscore/{playerId}")]
        public IActionResult GetHighScore(int playerId)
        {
            var highScore = _highScoreService.GetHighScore(playerId);

            if (highScore != null)
            {
                return Ok(new { player = highScore.Player, highscore = highScore.HighScore });
            }

            return NotFound($"High score not found for player with ID: {playerId}");
        }

        [HttpPost("savehighscore/{playerId}")]
        public IActionResult SaveHighScore(int playerId, int highscore)
        {
            _highScoreService.SaveHighScore(playerId, highscore);

            return Ok(new { player = playerId, highscore });
        }
    }
}
