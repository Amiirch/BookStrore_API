using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Dtos.Auth;

public class SignUpRequest
{
    [Required(ErrorMessage = "firstName is required")]
    [StringLength(50, ErrorMessage = "firstname must be between 10 and 50 characters", MinimumLength = 10)] 
    public string FullName { get; set; }
    
    [Required(ErrorMessage = "username is required")]
    [StringLength(50, ErrorMessage = "username must be between 10 and 50 characters", MinimumLength = 6)]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "password is required")]
    [StringLength(50, ErrorMessage = "password must be between 10 and 50 characters", MinimumLength = 10)]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "email is required")]
    [EmailAddress(ErrorMessage = "email is not valid")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "phoneNumber is required")]
    [Phone(ErrorMessage = "phoneNumber is not valid ")]
    public string PhoneNumber { get; set; }
}