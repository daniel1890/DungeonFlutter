using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IGameService
    {
        World StartGame(int rows, int columns);
        void SaveHighScore(int playerId, int highscore);
        HighScoreDTO? GetHighScore(int playerId);

        World GetWorld();
    }
}
