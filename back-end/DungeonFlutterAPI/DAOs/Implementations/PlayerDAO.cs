using DungeonFlutterAPI.DAOs.Interfaces;
using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.DAOs.Implementations
{
    public class PlayerDAO : IPlayerDAO
    {
        private readonly MyDbContext _dbContext;

        public PlayerDAO(MyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public bool IsPlayerNameTaken(string playerName)
        {
            return _dbContext.Players.Any(p => p.PlayerName == playerName);
        }

        public void RegisterPlayer(Player player)
        {
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
        }

        public Player? LoginPlayer(PlayerLoginDTO loginDTO)
        {

            var player = _dbContext.Players.FirstOrDefault(p =>
                 p.PlayerName == loginDTO.PlayerName && p.Password == loginDTO.Password);

            return player;
        }
    }
}
