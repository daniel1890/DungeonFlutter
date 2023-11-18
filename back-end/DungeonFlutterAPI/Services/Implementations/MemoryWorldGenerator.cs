using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Services.Interfaces;

namespace DungeonFlutterAPI.Services.Implementations
{
    public class MemoryWorldGenerator : IWorldGenerator
    {
        public World GenerateWorld(int rows, int columns)
        {
            if (rows % 2 != 0 || columns % 2 != 0)
            {
                throw new ArgumentException("The number of rows and columns must be even for a concentration game.");
            }

            List<List<int>> board = new List<List<int>>();

            List<int> numbers = Enumerable.Range(0, (rows * columns) / 2).ToList();

            Random random = new Random();
            numbers = numbers.OrderBy(x => random.Next()).ToList();

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
