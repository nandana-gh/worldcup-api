using Microsoft.EntityFrameworkCore;
using WorldCup.API.Models;

namespace WorldCup.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One vote per user constraint
            modelBuilder.Entity<Poll>()
                .HasIndex(p => p.UserId)
                .IsUnique();

            // Team unique constraints
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.TeamName)
                .IsUnique();
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.TeamCode)
                .IsUnique();
        }
    }
}
