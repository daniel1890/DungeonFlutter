using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IHighScoreService
    {
        void SaveHighScore(int playerId, int highscore);
        HighScoreDTO? GetHighScore(int playerId);
    }
}
