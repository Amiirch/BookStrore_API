using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Dtos.Auth;

public class SignInRequest
{
    [Required(ErrorMessage = "username is required")]
    [StringLength(50, ErrorMessage = "username must be between 10 and 50 characters", MinimumLength = 6)]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "password is required")]
    [StringLength(50, ErrorMessage = "password must be between 10 and 50 characters", MinimumLength = 8)]
    public string Password { get; set; }
}