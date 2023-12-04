using DungeonFlutterAPI.DAOs.Implementations;
using DungeonFlutterAPI.DAOs.Interfaces;
using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Exceptions;
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

        public void DeletePlayer(int playerId)
        {
            var player = _playerDAO.GetPlayerById(playerId);

            if (player == null)
            {
                throw new PlayerNotFoundException(); // You should create this exception class
            }

            _playerDAO.DeletePlayer(player);
        }
    }
}
