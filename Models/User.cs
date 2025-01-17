using System.Text.Json.Serialization;

namespace BookStoreApi.Models;

public class User
{
    public int Id { get; set; }
    
    public string FullName { get; set; }

    public string UserName { get; set; }
    
    [JsonIgnore]
    public string Password { get; set; }

    public string[] Roles { get; set; }
    
    public string Email { get; set; }
    
    public string phoneNumber { get; set; }
}