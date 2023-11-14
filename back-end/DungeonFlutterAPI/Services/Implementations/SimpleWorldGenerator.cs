using System.Security.Cryptography;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Services.Interfaces;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class SimpleWorldGenerator : IWorldGenerator
    {
        public World GenerateWorld()
        {
            World world = new World(1000, 1000);

            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    world.Tiles[x, y] = RandomNumberGenerator.GetInt32(0, 1);
                }
            }

            return world;
        }
    }
}
