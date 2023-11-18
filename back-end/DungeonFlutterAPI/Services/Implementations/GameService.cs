using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;
using DungeonFlutterAPI.Services.Interfaces;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IWorldGenerator _worldGenerator;
        private readonly IGame _game;
        private readonly MyDbContext _dbContext;

        public GameService(IWorldGenerator worldGenerator, IGame game, MyDbContext dbContext)
        {
            _worldGenerator = worldGenerator ?? throw new ArgumentNullException(nameof(worldGenerator));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _dbContext = dbContext;
        }

        public World StartGame(int rows, int columns)
        {
            _game.SetWorldGenerator(_worldGenerator);
            
            return _game.StartGame(rows, columns);
        }

        public World GetWorld()
        {
            return _game.GetWorld();
        }

        public void SaveHighScore(int playerId, int highscore)
        {
            var player = _dbContext.Players.FirstOrDefault(p => p.Id == playerId);
            if (player == null)
            {
                throw new ArgumentException();
            }

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

            // No highscore found
            return null;
        }
    }
}
