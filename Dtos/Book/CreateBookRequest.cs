using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Extensions.Dtos.Book;

public class CreateBookRequest
{
    [Required(ErrorMessage = "Book name is required")]
    [StringLength(50, ErrorMessage = "Book name must be between 1 and 50 characters", MinimumLength = 5)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "book author is required")]
    [StringLength(50, ErrorMessage = "book author must be between 10 and 50 characters", MinimumLength = 10)]
    public string Author { get; set; }
    
    [Required(ErrorMessage = "book Genre is required")]
    [StringLength(50, ErrorMessage = "book Genre must be between 5 and 50 characters", MinimumLength = 5)]
    public string Genre { get; set; }
    
    [Required(ErrorMessage = "book price is required")]
    public float Price { get; set; }
    
    public int Stock { get; set; }
    
}