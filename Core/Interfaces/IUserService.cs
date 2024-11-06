using Core.Models;

namespace Core.Interfaces;
/// <summary>
/// Adapter for IUserRepository that connect the API -> Services,and  handles user-related operations,
/// including authentication and token generation.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Get All Users Adapter
    /// </summary>
    /// <returns>List Of Users</returns>
    Task<IEnumerable<User>> GetAllUsersAsync();
    
    /// <summary>
    /// Get User By id Adapter
    /// </summary>
    /// <param name="id">User Id Guid</param>
    /// <returns>A User if found, or null if not</returns>
    Task<User?> GetUserByIdAsync(Guid id);
    
    /// <summary>
    /// Create New User Adapter
    /// </summary>
    /// <param name="user">New User Entity to be created</param>
    /// <returns></returns>
    Task AddUserAsync(User user);
    
    /// <summary>
    /// Update a User Adapter
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
    /// With the user credentials authenticate found with the mail, and verify with the password hashed
    /// </summary>
    /// <param name="userLoginRequest">email and password</param>
    /// <returns>The User if the credentials are valid, null if not</returns>
    Task<User?> AuthenticateAsync(UserLoginRequest userLoginRequest);
    
    /// <summary>
    /// Generate a Toke with JWT with expiration time
    /// </summary>
    /// <param name="user">Credentials for the Token</param>
    /// <returns>Token JWT</returns>
    public string GenerateToken(User user);
}