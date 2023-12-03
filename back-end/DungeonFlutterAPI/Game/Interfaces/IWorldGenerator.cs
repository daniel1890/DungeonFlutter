using DungeonFlutterAPI.Models.Domain;

namespace DungeonFlutterAPI.Game.Interfaces
{
    public interface IWorldGenerator
    {
        World GenerateWorld(int rows, int columns);
    }
}