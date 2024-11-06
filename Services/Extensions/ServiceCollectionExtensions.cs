using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using Data.Repositories;

namespace Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        // Register User Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>(); 
    }
}