using Core.Models;

namespace Core.Interfaces;

/// <summary>
/// Defines the contract for a repository that interacts with User entities using the UsersDbContext.
/// This interface provides methods for retrieving, adding, updating, and deleting users.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>An Array of User Entities</returns>
    Task<IEnumerable<User>> GetAllUsersAsync();
    
    /// <summary>
    /// Get a user with the same id provided
    /// </summary>
    /// <param name="id">User Id Guid</param>
    /// <returns>A user if found, or null if not</returns>
    Task<User?> GetUserByIdAsync(Guid id);
    
    /// <summary>
    /// Create a new User on the Database
    /// </summary>
    /// <param name="user">New User Entity to be created</param>
    /// <returns></returns>
    Task AddUserAsync(User user);
    
    /// <summary>
    /// Update the product provided, and found with the id
    /// If the password is no passed, only update the name, mail
    /// </summary>
    /// <param name="user">User to be Updated</param>
    /// <returns></returns>
    Task UpdateUserAsync(User user);
    
    /// <summary>
    /// Set isActive to 0
    /// </summary>
    /// <param name="id">Guid of the User to be Disabled</param>
    /// <returns></returns>
    Task DeleteUserAsync(Guid id);
    
    /// <summary>
    /// Find a User by the Email
    /// </summary>
    /// <param name="email">Email of the User to be found</param>
    /// <returns>The User founded or Null if not</returns>
    Task<User?> GetUserByEmailAsync(string email);
}