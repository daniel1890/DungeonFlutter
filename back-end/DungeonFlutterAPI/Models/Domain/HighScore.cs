namespace DungeonFlutterAPI.Models.Domain
{
    public class HighScore
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int Score { get; set; }
    }
}
