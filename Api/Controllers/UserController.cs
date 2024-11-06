using System.IdentityModel.Tokens.Jwt;
using Core.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace Api.Controllers;

[ApiController]
[Route("/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Logs in a user and returns a JSON Web Token (JWT) for successful authentication
    /// </summary>
    /// <remarks>
    /// This method expects a UserLoginRequest object containing the user email and password in the request body
    /// On successful authentication, a JWT token containing user claims and an expiration time is generated and
    /// returned
    /// 
    /// ## Security Considerations: ##
    /// - The password is never transmitted in plain text.
    /// </remarks>
    /// <param name="loginRequest">The user login credentials (email and password).</param>
    /// <returns>
    /// Status 200 with and Object containing either a TokenResponse object with the token and expiration time on
    /// success, or an Unauthorized status code on failure
    /// </returns>
    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>>  Login([FromBody] UserLoginRequest loginRequest)
    {
        var user = await userService.AuthenticateAsync(loginRequest);
        
        if (user == null)
        {
            return Unauthorized(); 
        }

        var token = userService.GenerateToken(user);
        // Get the Expiration Time from claims
        var expirationDate = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

        return Ok(new 
        { 
            token,
            expiration = expirationDate?.ValidTo
        });
    }
    
    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <remarks>
    /// This method expects a UserRequest object containing the user, name, email, and password in the request body.
    /// The password is hashed using BCrypt before creating the user.
    /// </remarks>
    /// <param name="userRequest">The user data to be created (name, email, and password).</param>
    /// <returns>
    /// Status 200 with an Object containing the newly created User object on success, or a BadRequest status code
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<User>> PostUser([FromBody] UserRequest userRequest)
    {
        var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(userRequest.Password, 13);
            
        var user = new User(userRequest.Name, userRequest.Email, passwordHash);
            
        await userService.AddUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    
    /// <summary>
    /// Retrieves all users from UserDb/UserProdDb
    /// </summary>
    /// <remarks>
    /// This method retrieves all users from the data store and returns them as an Array of User objects.
    /// 
    /// </remarks>
    /// <returns>
    /// Status 200 with an Array of User objects on success, or 404 not Found status code on failure
    /// (no users found).
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    /// <summary>
    /// Updates an existing user information.
    /// </summary>
    /// <remarks>
    /// This method updates the User with the request name, email, and optionally password for the provided user id.
    /// 
    /// **Security Considerations:**
    /// * If updating passwords, ensure the new password is hashed securely using BCrypt.
    /// </remarks>
    /// <param name="id">The unique identifier of the user to update</param>
    /// <param name="userRequest">The updated user data (name, email, and optional password)</param>
    /// <returns>
    /// Status 200 With the updated User object on success, or a NotFound status code on failure (user not found)
    /// </returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<User>> PutUser(Guid id,[FromBody] UserRequest userRequest)
    {
        var user = await userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        user.Name = userRequest.Name;
        user.Email = userRequest.Email;
        if (!string.IsNullOrEmpty(userRequest.Password))
        {
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userRequest.Password, 13);
        }

        await userService.UpdateUserAsync(user);

        return Ok(user);
    }

    /// <summary>
    /// Soft deletes (Inability) a user by setting their IsActive flag to false
    /// </summary>
    /// <remarks>
    /// This method doesn't permanently delete the user from the system, but marks them as inactive.
    /// </remarks>
    /// <param name="id">The unique identifier of the user to delete (soft delete)</param>
    /// <returns>
    /// 200 status code on success (user marked inactive) or a NotFound status code on failure (user not found).
    /// </returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        user.IsActive = false;
        await userService.UpdateUserAsync(user);

        return Ok(user);
    }
}