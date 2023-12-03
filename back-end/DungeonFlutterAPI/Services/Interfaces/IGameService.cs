using DungeonFlutterAPI.Models.Domain;
using DungeonFlutterAPI.Models.DTO;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IGameService
    {
        World StartGame(int rows, int columns);

        World GetWorld();
    }
}
