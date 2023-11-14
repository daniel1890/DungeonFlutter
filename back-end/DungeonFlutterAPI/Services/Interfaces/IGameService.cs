using DungeonFlutterAPI.Models;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IGameService
    {
        World StartGame();
        void MoveCharacter(string direction);
        void SaveHighScore(string playerName, int score);
        World GetWorld();
    }
}
