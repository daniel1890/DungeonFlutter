using DungeonFlutterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DungeonFlutterAPI.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<HighScore> HighScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the SQLite database connection string
            optionsBuilder.UseSqlite("Data Source=dungeon_flutter.db");
        }
    }
}
