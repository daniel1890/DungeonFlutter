using DungeonFlutterAPI.Models.Domain;

namespace DungeonFlutterAPI.Models.DTO
{
    public class HighScoreDTO
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int HighScore { get; set; }
    }
}
