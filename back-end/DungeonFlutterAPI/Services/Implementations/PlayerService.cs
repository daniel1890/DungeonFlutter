using DungeonFlutterAPI.DAOs;
using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;
using DungeonFlutterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly PlayerDAO _playerDAO;

        public PlayerService(PlayerDAO playerDAO)
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
