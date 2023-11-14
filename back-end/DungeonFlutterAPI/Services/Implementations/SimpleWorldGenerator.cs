using System.Security.Cryptography;
using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Services.Interfaces;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class SimpleWorldGenerator : IWorldGenerator
    {
        /*public World GenerateWorld()
        {
            World world = new World(1000, 1000);

            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    world.Tiles[x, y] = RandomNumberGenerator.GetInt32(0, 2);
                }
            }

            return world;
        }*/

        public World GenerateWorld(int rows, int columns)
        {
            if (rows % 2 != 0 || columns % 2 != 0)
            {
                throw new ArgumentException("The number of rows and columns must be even for a concentration game.");
            }

            List<List<int>> board = new List<List<int>>();

            // Create a list of numbers from 0 to (rows*columns)/2
            List<int> numbers = Enumerable.Range(0, (rows * columns) / 2).ToList();

            // Shuffle the numbers
            Random random = new Random();
            numbers = numbers.OrderBy(x => random.Next()).ToList();

            // Populate the board with pairs of numbers
            for (int i = 0; i < rows; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < columns; j++)
                {
                    row.Add(numbers[i * columns / 2 + j % (columns / 2)]);
                }
                board.Add(row);
            }

            World world = new World(columns, rows);
            world.SetTiles(board);

            return world;
        }
    }
}
