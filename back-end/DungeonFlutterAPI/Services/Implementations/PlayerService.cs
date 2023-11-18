using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly MyDbContext _dbContext;

        public PlayerService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
