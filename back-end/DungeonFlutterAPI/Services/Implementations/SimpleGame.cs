using DungeonFlutterAPI.Models;
using DungeonFlutterAPI.Services.Interfaces;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class SimpleGame : IGame
    {
        private IWorldGenerator? worldGenerator;
        private World? world;

        public SimpleGame() { }

        public void MoveCharacter(string direction)
        {
            throw new NotImplementedException();
        }

        public void SetWorldGenerator(IWorldGenerator worldGenerator)
        {
            this.worldGenerator = worldGenerator;
            world = null;
        }

        public World StartGame()
        {
            if (worldGenerator == null)
            {
                throw new InvalidOperationException("WorldGenerator not set");
            }

            world = worldGenerator.GenerateWorld();
            Console.WriteLine($"Game started with world: {world}");

            return world;
        }

        World IGame.GetWorld()
        {
            return world;
        }
    }
}