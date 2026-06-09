using Microsoft.EntityFrameworkCore;
using WorldCup.API.Models;

namespace WorldCup.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate(); // Apply migrations automatically

            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                var admin = new User
                {
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Role = "Admin"
                };
                context.Users.Add(admin);
            }

            if (!context.Teams.Any())
            {
                var teams = new List<Team>
                {
                    new Team { TeamName = "Argentina", TeamCode = "ARG", GroupName = "A", FlagImageUrl = "https://flagcdn.com/w320/ar.png", Description = "Defending Champions", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new Team { TeamName = "Brazil", TeamCode = "BRA", GroupName = "G", FlagImageUrl = "https://flagcdn.com/w320/br.png", Description = "Five-time champions", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new Team { TeamName = "France", TeamCode = "FRA", GroupName = "D", FlagImageUrl = "https://flagcdn.com/w320/fr.png", Description = "2018 Champions", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new Team { TeamName = "Germany", TeamCode = "GER", GroupName = "E", FlagImageUrl = "https://flagcdn.com/w320/de.png", Description = "Four-time champions", IsActive = true, CreatedAt = DateTime.UtcNow }
                };
                context.Teams.AddRange(teams);
            }

            if (!context.SystemSettings.Any())
            {
                context.SystemSettings.Add(new SystemSetting
                {
                    IsResultPublished = false,
                    PollClosingDate = null
                });
            }

            context.SaveChanges();
        }
    }
}
