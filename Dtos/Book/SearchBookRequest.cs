namespace BookStoreApi.Extensions.Dtos.Book;

public class SearchBookRequest
{
    public string? Gerne { get; set; }
    public float? MinPrice { get; set; }
    public float? MaxPrice { get; set; }
}