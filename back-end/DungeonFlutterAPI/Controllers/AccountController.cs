using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using DungeonFlutterAPI.Services.Interfaces;

namespace DungeonFlutterAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public AccountController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] PlayerRegistrationDTO registrationDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(registrationDTO.PlayerName) || string.IsNullOrEmpty(registrationDTO.Password))
                {
                    return BadRequest("PlayerName and Password are required.");
                }

                if (_playerService.IsPlayerNameTaken(registrationDTO.PlayerName))
                {
                    return BadRequest("PlayerName is already taken.");
                }

                var player = new Player
                {
                    PlayerName = registrationDTO.PlayerName,

                    Password = registrationDTO.Password
                };

                _playerService.RegisterPlayer(player);

                return Ok(new { playerName = player.PlayerName, Id = player.Id });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] PlayerLoginDTO loginDTO)
        {
            var player = _playerService.LoginPlayer(loginDTO);

            if (player != null)
            {
                return Ok(new { playerName = player.PlayerName, Id = player.Id });
            }

            return BadRequest("Invalid credentials");
        }
    }
}
