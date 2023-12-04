using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.DAOs.Interfaces
{
    public interface IPlayerDAO
    {
        public bool IsPlayerNameTaken(string playerName);

        public void RegisterPlayer(Player player);

        public Player? LoginPlayer(PlayerLoginDTO loginDTO);

        public Player? GetPlayerById(int playerId);

        public void DeletePlayer(Player player);
    }
}
