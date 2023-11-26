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

            // Use Fisher-Yates shuffle for shuffling the numbers list
            Random random = new Random();
            int n = numbers.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int value = numbers[k];
                numbers[k] = numbers[n];
                numbers[n] = value;
            }

            numbers.AddRange(numbers);

            n = numbers.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int value = numbers[k];
                numbers[k] = numbers[n];
                numbers[n] = value;
            }

            for (int i = 0; i < rows; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < columns; j++)
                {
                    row.Add(numbers[i * columns + j]);
                }
                board.Add(row);
            }

            World world = new World(columns, rows);
            world.SetTiles(board);

            return world;
        }
    }
}
