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
