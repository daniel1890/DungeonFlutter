namespace DungeonFlutterAPI.Models.Domain
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string Password { get; set; }
        public List<HighScore> HighScores { get; set; }
    }
}
