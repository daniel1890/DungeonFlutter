using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Models;
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

        public World StartGame()
        {
            _game.SetWorldGenerator(_worldGenerator);
            
            return _game.StartGame();
        }

        public void MoveCharacter(string direction)
        {
            // Implement character movement logic
        }

        public World GetWorld()
        {
            return _game.GetWorld();
        }

        public void SaveHighScore(string playerName, int score)
        {
            var highScore = new HighScore
            {
                PlayerName = playerName,
                Score = score
            };

            _dbContext.HighScores.Add(highScore);
            _dbContext.SaveChanges();
        }
    }
}
