using DungeonFlutterAPI.DAOs.Implementations;
using DungeonFlutterAPI.DAOs.Interfaces;
using DungeonFlutterAPI.Data;
using DungeonFlutterAPI.Game.Interfaces;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;
using DungeonFlutterAPI.Services.Interfaces;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IWorldGenerator _worldGenerator;
        private readonly IGame _game;

        public GameService(IWorldGenerator worldGenerator, IGame game, IHighScoreDAO highScoreDAO)
        {
            _worldGenerator = worldGenerator ?? throw new ArgumentNullException(nameof(worldGenerator));
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public World StartGame(int rows, int columns)
        {
            _game.SetWorldGenerator(_worldGenerator);
            
            return _game.StartGame(rows, columns);
        }

    }
}
