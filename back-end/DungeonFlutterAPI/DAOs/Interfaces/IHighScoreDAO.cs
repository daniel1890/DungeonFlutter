using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.DAOs.Interfaces
{
    public interface IHighScoreDAO
    {
        public void SaveHighScore(int playerId, int highscore);

        HighScoreDTO? GetHighScore(int playerId);
    }
}
