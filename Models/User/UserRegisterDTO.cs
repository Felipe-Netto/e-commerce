using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models.User;

public class UserRegisterDTO
{
    [Required] public string Name { get; set; } = null!;

    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required] [MinLength(6)] public string Password { get; set; } = null!;
    
    public string Role { get; set; } = "User";
    
    [Phone] public string Phone { get; set; } = null!;
}