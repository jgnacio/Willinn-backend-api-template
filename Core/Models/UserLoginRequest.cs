namespace Core.Models;
/// <summary>
/// User Login Properties. These properties are validated for the login endpoint
/// </summary>
public class UserLoginRequest
{
        public required string Email { get; set; }
        public required string Password { get; set; }
}