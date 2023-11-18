using DungeonFlutterAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DungeonFlutterAPI.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<HighScore> HighScores { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=dungeon_flutter.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HighScore>()
                .HasOne(hs => hs.Player)
                .WithMany(p => p.HighScores)
                .HasForeignKey(hs => hs.PlayerId);
        }
    }
}
