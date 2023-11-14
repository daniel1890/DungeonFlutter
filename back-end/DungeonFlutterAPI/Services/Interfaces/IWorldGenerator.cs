using DungeonFlutterAPI.Models.Domain;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IWorldGenerator
    {
        World GenerateWorld();
    }
}