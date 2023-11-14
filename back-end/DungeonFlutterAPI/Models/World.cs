namespace DungeonFlutterAPI.Models
{
    public class World
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public int[,]? Tiles { get; set; }

        public World(int Width, int Height)
        {
            Tiles = new int[Width, Height];
        }
    }
}