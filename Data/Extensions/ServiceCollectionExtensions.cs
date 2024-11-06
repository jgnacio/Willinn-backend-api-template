using Data.Persistence;
using Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions;

/// <summary>
/// Provides extension methods for configuring data infrastructure services in an IServiceCollection
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Get Database connection string from appsettings
        var connectionString = configuration.GetConnectionString("WillinnBackendApiDb");
        services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

        // Add the UserSeeder service for database initialization for Data initialization for Development environment
        services.AddScoped<IUserSeeder, UserSeeder>();
    }
}