using BookStoreApi.Extensions.Dtos.Book;
using BookStoreApi.Models;

namespace BookStoreApi.Services.contracts;

public interface IBookService
{
    Task<Book> GetById(int productId);
    Task CreateAsync(CreateBookRequest createProductRequest);
    Task<Book> UpdateAsync(UpdateBookRequest updateUserRequest);
    Task DeleteAsync(int id);
    Task<List<Book>> GetPopularAsync();
    List<Book> SearchAsync(SearchBookRequest searchBookRequest);
}