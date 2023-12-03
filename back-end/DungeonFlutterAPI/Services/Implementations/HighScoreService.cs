using DungeonFlutterAPI.DAOs.Interfaces;
using DungeonFlutterAPI.Models.DTO;
using DungeonFlutterAPI.Services.Interfaces;
using System.Xml.Linq;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class HighScoreService : IHighScoreService
    {
        private readonly IHighScoreDAO _highScoreDAO;
        public HighScoreService(IHighScoreDAO highScoreDAO)
        {
            _highScoreDAO = highScoreDAO ?? throw new ArgumentNullException(nameof(_highScoreDAO));
        }
        public void SaveHighScore(int playerId, int highscore)
        {
            _highScoreDAO.SaveHighScore(playerId, highscore);
        }

        public HighScoreDTO? GetHighScore(int playerId)
        {
            return _highScoreDAO.GetHighScore(playerId);
        }
    }
}
