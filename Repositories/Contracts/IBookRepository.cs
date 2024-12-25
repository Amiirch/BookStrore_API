using BookStoreApi.Extensions.Dtos.Book;
using BookStoreApi.Models;

namespace BookStoreApi.Repositories.Contracts;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id);
    
    Task<Book> CreateAsync(Book book);
    
    Task<bool> GetExistByNameAsync(string name);
    
    Task<int> DeleteAsync(Book book);
    
    Task<Book> UpdateAsync(Book book);

    List<Book>? SearchAsync(SearchBookRequest searchBookRequest);
}