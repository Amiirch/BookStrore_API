using BookStoreApi.Data;
using BookStoreApi.Exceptions;
using BookStoreApi.Extensions.Dtos.Book;
using BookStoreApi.Models;
using BookStoreApi.Repositories.Contracts;
using BookStoreApi.Services.contracts;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BookStoreApi.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    
    public BookService(IBookRepository bookRepository,RedisService redisService,IConfiguration configuration)
    {
        _bookRepository = bookRepository;
        _redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
        _db = _redis.GetDatabase();
    }

    public async Task<Book> GetById(int id)
    {
        Book book;
        string cacheKey = $"book:{id}";
        
        var cachedBook = await _db.StringGetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedBook))
        {
            book = JsonConvert.DeserializeObject<Book>(cachedBook);
            return book;
        }
        
        book = await _bookRepository.GetByIdAsync(id);
        
        if (book == null)
        {
            throw new NotFoundException("book with this id isn't exist", 404);
        }
        
        var serializedBook = JsonConvert.SerializeObject(book);
        await _db.StringSetAsync($"book:{book.Id}",serializedBook);
        await _db.SortedSetIncrementAsync("popular:books", serializedBook, 1);
        
        return book;
    }

    public async Task CreateAsync(CreateBookRequest createBookRequest)
    {
        var existBook = await _bookRepository.GetExistByNameAsync(createBookRequest.Name);
        if (existBook) throw new DuplicateException("book with this name is exist", 409);
        
        var book = new Book();
        book.Name = createBookRequest.Name;
        book.Author = createBookRequest.Author;
        book.Genre = createBookRequest.Genre;
        book.Price = createBookRequest.Price;
        book.Stock = createBookRequest.Stock;
        
        await _bookRepository.CreateAsync(book);
    }

    public async Task<Book> UpdateAsync(UpdateBookRequest updateBookRequest)
    {
        var existBook = await _bookRepository.GetByIdAsync(updateBookRequest.Id);
        if (existBook == null) throw new NotFoundException("Book with this Id isn't exist", 404);
        
        existBook.Name = updateBookRequest.Name;
        existBook.Author = updateBookRequest.Author;
        existBook.Genre = updateBookRequest.Genre;
        existBook.Price = updateBookRequest.Price;
        existBook.Stock = updateBookRequest.Stock;
        
        var updatedBook = await _bookRepository.UpdateAsync(existBook);
        if (_db.KeyExists($"book:{updateBookRequest.Id}"))
        {
            await _db.KeyDeleteAsync($"book:{updateBookRequest.Id}");
            var serializedBook = JsonConvert.SerializeObject(updatedBook);
            await _db.StringSetAsync($"book:{updatedBook.Id}",serializedBook);
        }
        var popularBooks = await _db.SortedSetRangeByScoreAsync("popular:books");
        
        foreach (var serializedBook in popularBooks)
        {
            var book = JsonConvert.DeserializeObject<Book>(serializedBook);
            if (book.Id == updateBookRequest.Id)
            {
                await _db.SortedSetRemoveAsync("popular:books", serializedBook);
                await _db.SortedSetIncrementAsync("popular:books", JsonConvert.SerializeObject(updatedBook), 1);

                break;
            }
        }
        return updatedBook;
    }
    
    public async Task DeleteAsync(int id)
    {
        var existBook = await _bookRepository.GetByIdAsync(id);
        if (existBook == null) throw new NotFoundException("book with this id is not exist", 404);
     
        if (_db.KeyExists($"book:{id}")) await _db.KeyDeleteAsync($"book:{id}");
        
        var popularBooks = await _db.SortedSetRangeByScoreAsync("popular:books");
        
        foreach (var serializedBook in popularBooks)
        {
            var book = JsonConvert.DeserializeObject<Book>(serializedBook);
            if (book.Id == id)
            {
                await _db.SortedSetRemoveAsync("popular:books", serializedBook);
                break;
            }
        }

        await _bookRepository.DeleteAsync(existBook);
    }

    public async Task<List<Book>> GetPopularAsync()
    {
        
        var popularBooks = await _db.SortedSetRangeByScoreWithScoresAsync(
            "popular:books", 
            order: Order.Descending, 
            take: 10
        );

        var books = popularBooks.Select(entry =>
            JsonConvert.DeserializeObject<Book>(entry.Element)
        ).ToList();

        return books;
    }
    public  List<Book> SearchAsync(SearchBookRequest searchBookRequest)
    {
        var books =  _bookRepository.SearchAsync(searchBookRequest);
        if (books == null) throw new NotFoundException("book with this id isn't exist", 404);
        return books;
    }
}