namespace DungeonFlutterAPI.Models.Domain
{
    public class World
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public List<List<int>> board { get; internal set; }

        public World(int width, int height)
        {
            Width = width;
            Height = height;
            this.board = new List<List<int>>();
        }

        public void SetTiles(List<List<int>> board)
        {
            if (board.Count != Height || board[0].Count != Width)
            {
                throw new ArgumentException("Invalid board size");
            }

            this.board = board;
        }
    }
}