using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IGameService
    {
        World StartGame();
        void MoveCharacter(string direction);
        void SaveHighScore(string playerName, int score);
        HighScoreDTO? GetHighScore(string playerName);

        World GetWorld();
    }
}
