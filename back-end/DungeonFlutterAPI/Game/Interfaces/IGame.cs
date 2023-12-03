using DungeonFlutterAPI.Models.Domain;

namespace DungeonFlutterAPI.Game.Interfaces
{
    public interface IGame
    {
        World GetWorld();
        void SetWorldGenerator(IWorldGenerator worldGenerator);
        World StartGame(int rows, int columns);
    }
}