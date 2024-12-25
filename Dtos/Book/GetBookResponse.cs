namespace BookStoreApi.Extensions.Dtos.Book;

public class GetBookResponse
{
    public GetBookResponse(int id,string name,string author,string genre,float price ,int stock)
    {
        Id = id;
        Name = name;
        Author = author;
        Genre = genre;
        Price = price;
        Stock = stock;
    }
    public int Id { get; set;}
    public string Name { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
}