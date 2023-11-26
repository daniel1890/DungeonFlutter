using DungeonFlutterAPI.DAOs;
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
        private readonly HighScoreDAO _highScoreDAO;

        public GameService(IWorldGenerator worldGenerator, IGame game, HighScoreDAO highScoreDAO)
        {
            _worldGenerator = worldGenerator ?? throw new ArgumentNullException(nameof(worldGenerator));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _highScoreDAO = highScoreDAO ?? throw new ArgumentNullException(nameof(_highScoreDAO));
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
            _highScoreDAO.SaveHighScore(playerId, highscore);
        }

        public HighScoreDTO? GetHighScore(int playerId)
        {
            return _highScoreDAO.GetHighScore(playerId);
        }
    }
}
