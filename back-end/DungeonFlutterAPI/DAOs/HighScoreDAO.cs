using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.DAOs
{
    public class HighScoreDAO
    {
        private readonly MyDbContext _dbContext;

        public HighScoreDAO(MyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void SaveHighScore(int playerId, int highscore)
        {
            var player = _dbContext.Players.FirstOrDefault(p => p.Id == playerId) ?? throw new ArgumentException();
            var highScore = new HighScore
            {
                PlayerId = playerId,
                Player = player,
                Score = highscore
            };

            _dbContext.HighScores.Add(highScore);
            _dbContext.SaveChanges();
        }

        public HighScoreDTO? GetHighScore(int playerId)
        {
            var highScore = _dbContext.HighScores
                .Where(h => h.PlayerId == playerId)
                .OrderByDescending(h => h.Score)
                .FirstOrDefault();

            if (highScore != null)
            {
                var player = _dbContext.Players.FirstOrDefault(p => p.Id == playerId);

                if (player != null)
                {
                    return new HighScoreDTO
                    {
                        Player = player,
                        HighScore = highScore.Score
                    };
                }
            }

            return null;
        }
    }
}
