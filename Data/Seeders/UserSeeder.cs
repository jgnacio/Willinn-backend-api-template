using Core.Models;
using Data.Persistence;
using BCrypt.Net;

namespace Data.Seeders;

/// <summary>
/// Populates the Users table with initial data during application startup (Only first Time when no have Users)
/// </summary>
internal class UserSeeder(UsersDbContext dbContext) : IUserSeeder
{
    public async Task Seed()
    {
        // Checks if the database connection is available before seeding.Seeds initial users
        // if the Users table is empty.
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Users.Any())
            {
                var users = GetUsers();
                dbContext.Users.AddRange(users);
                await dbContext.SaveChangesAsync();
            }
            
        }
    }

    private IEnumerable<User> GetUsers()
    {
        // Initial User for initial data and Development
        List<User> users = [
            new(
                "WillinUserGuest",
                "WillinnGuest@gmail.com",
                BCrypt.Net.BCrypt.EnhancedHashPassword("Guest")
            )
            // This User is for Test Without have to create a User When init The app without any data
        ];
        return users;
        
    }

}