using DungeonFlutterAPI.Models.Domain;

namespace DungeonFlutterAPI.Services.Interfaces
{
    public interface IGame
    {
        World GetWorld();
        void SetWorldGenerator(IWorldGenerator worldGenerator);
        World StartGame(int rows, int columns);
        void MoveCharacter(string direction);

    }
}