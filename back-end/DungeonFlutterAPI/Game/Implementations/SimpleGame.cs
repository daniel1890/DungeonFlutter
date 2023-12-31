using DungeonFlutterAPI.Game.Interfaces;
using DungeonFlutterAPI.Models.Domain;

namespace DungeonFlutterAPI.Game.Implementations
{
    public class SimpleGame : IGame
    {
        private IWorldGenerator? worldGenerator;
        private World? world;

        public SimpleGame() { }

        public void SetWorldGenerator(IWorldGenerator worldGenerator)
        {
            this.worldGenerator = worldGenerator;
            world = null;
        }

        public World StartGame(int rows, int columns)
        {
            if (worldGenerator == null)
            {
                throw new InvalidOperationException("WorldGenerator not set");
            }


            World world = worldGenerator.GenerateWorld(rows, columns);
            List<List<int>> board = world.board;
            Console.WriteLine($"Game started with world: {world}");

            return world;
        }

        World IGame.GetWorld()
        {
            return world;
        }
    }
}