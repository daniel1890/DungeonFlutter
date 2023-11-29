using DungeonFlutterAPI.DAOs.Implementations;
using DungeonFlutterAPI.DAOs.Interfaces;
using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;
using DungeonFlutterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerDAO _playerDAO;

        public PlayerService(IPlayerDAO playerDAO)
        {
            _playerDAO = playerDAO;
        }

        public void RegisterPlayer(Player player)
        {
            _playerDAO.RegisterPlayer(player);
        }

        public Player? LoginPlayer(PlayerLoginDTO loginDTO)
        {
            return _playerDAO.LoginPlayer(loginDTO);
        }

        public bool IsPlayerNameTaken(string playerName)
        {
            return _playerDAO.IsPlayerNameTaken(playerName);
        }
    }
}
