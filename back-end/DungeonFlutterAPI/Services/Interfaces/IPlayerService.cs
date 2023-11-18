using DungeonFlutterAPI.Models.Domain;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        bool IsPlayerNameTaken(string playerName);
        void RegisterPlayer(Player player);
    }
}
