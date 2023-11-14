using DungeonFlutterAPI.Models;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IWorldGenerator
    {
        World GenerateWorld();
    }
}