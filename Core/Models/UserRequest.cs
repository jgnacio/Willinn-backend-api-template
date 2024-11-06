namespace Core.Models;
/// <summary>
/// User Request properties for the Endpoints, only Name,Email,Password
/// </summary>
public class UserRequest
{
    public required string Name { get; set; }   
    public required string Email { get; set; }
    public required string Password { get; set; }
}