using BookStoreApi.Data;
using BookStoreApi.Extensions.Dtos.Book;
using BookStoreApi.Models;
using BookStoreApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services;

public class BookRepository: IBookRepository
{
    private readonly ApplicationDbContext _dataContext;

    public BookRepository(ApplicationDbContext dataContext)
    {
        _dataContext = dataContext;
    }
    public Task<Book?> GetByIdAsync(int id)
    {
        return _dataContext.Books
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> CreateAsync(Book book)
    { 
        await _dataContext.Books.AddAsync(book);
        await _dataContext.SaveChangesAsync(); 
        return book;
    }
    
    public async Task<bool> GetExistByNameAsync(string name)
    {
        return await _dataContext.Books.AnyAsync(b => b.Name == name);
    }
    public async Task<int> DeleteAsync(Book book)
    {
        _dataContext.Books.Remove(book);
        await _dataContext.SaveChangesAsync();
        return book.Id;
    }
    public async Task<Book> UpdateAsync(Book book)
    {
        _dataContext.Books.Update(book);
        await _dataContext.SaveChangesAsync();
        return book;
    }

    public List<Book>? SearchAsync(SearchBookRequest searchBookRequest)
    {
        var query = _dataContext.Books.AsQueryable();
        if (searchBookRequest.MinPrice != null) query = query.Where(b => b.Price >= searchBookRequest.MinPrice);
        if(searchBookRequest.MaxPrice != null) query = query.Where(b => b.Price <= searchBookRequest.MaxPrice);
        if (!string.IsNullOrEmpty(searchBookRequest.Gerne)) query = query.Where(b => b.Genre == searchBookRequest.Gerne);
       
        var books = query.ToList();
        return books;
    }
}