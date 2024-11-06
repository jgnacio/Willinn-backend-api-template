using Core.Models;
using Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }


    public async Task<User?> AuthenticateAsync(UserLoginRequest userLoginRequest)
    {
        // Authenticates a user based on their email and password.
        var user = await _userRepository.GetUserByEmailAsync(userLoginRequest.Email);
            
        try
        {
            if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(userLoginRequest.Password, user.Password))
            {
                return user;
            }
        }
        catch (Exception ex)
        {
            // Handle Error if BCrypt Throw an error
            throw new InvalidOperationException("Error al verificar la contraseña", ex);
        }

        return null; 
    }
    
    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }



    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
    
    /// <summary>
    /// Generate a Token (JWT)
    /// </summary>
    /// <param name="user">Credentials with User</param>
    /// <returns>The generated token string</returns>
    public string GenerateToken(User user)
    {
        // Load Config form appsettings/appsettings.Development
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creeds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}