namespace BookStoreApi.Models;

public class Book
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Author { get; set; }
    
    public string Genre { get; set; }
    
    public float Price { get; set; }
    
    public int Stock { get; set; }
}