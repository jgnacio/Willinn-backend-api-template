using System.ComponentModel.DataAnnotations;

namespace Core.Models;
/// <summary>
/// User Definitions
/// </summary>
public class User
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } 

    [Required]
    [EmailAddress]
    public string Email { get; set; } 

    public string Password { get; set; }
    public bool IsActive { get; set; } = true;
    
    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }
}