using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence;

/// <summary>
/// Represents the DbContext for the application user data
/// </summary>
public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Generate The Tables on UserDb/UserProdDb with Microsoft Entitle framework
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Set User entity model with a unique Email property
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}