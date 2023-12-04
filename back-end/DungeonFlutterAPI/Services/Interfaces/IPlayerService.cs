using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        bool IsPlayerNameTaken(string playerName);
        void RegisterPlayer(Player player);
        Player? LoginPlayer(PlayerLoginDTO loginDTO);
        void DeletePlayer(int playerId);
    }
}
