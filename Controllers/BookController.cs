using BookStoreApi.Dtos.User;
using BookStoreApi.Exceptions;
using BookStoreApi.Extensions.Dtos.Book;
using BookStoreApi.Services.contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;


[Route("api/[Controller]")]
[ApiController]
public class BookController: ControllerBase
{
     private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [Authorize(Roles = $"{UserRoles.User}")]
    [HttpGet("{bookId:int}")]
    public async Task<ActionResult> GetByIdAsync([FromRoute] int bookId)
    {
        try
        {
            var book = await _bookService.GetById(bookId);
            return Ok(book);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.ErrorCode, new { errormessage = ex.Message });
        }
    }

    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateBookRequest createBookRequest)
    {
        try
        {
            await _bookService.CreateAsync(createBookRequest);
            return Ok("book created successfully");
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.ErrorCode, new { errormessage = ex.Message });
        }
    }
    
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateBookRequest updateBookRequest)
    {
        try
        {
            await _bookService.UpdateAsync(updateBookRequest);
            return Ok("book updated successfully");
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.ErrorCode, new { errormessage = ex.Message });
        }
    }
    
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
    [HttpDelete("{bookId:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int bookId)
    {
        try
        {
            await _bookService.DeleteAsync(bookId);
            return Ok("book deleted successfully");
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.ErrorCode, new { errormessage = ex.Message });
        }
    }
    
    [Authorize(Roles = $"{UserRoles.User}")]
    [HttpGet("popular")]
    public async Task<ActionResult> GetPopularAsync()
    {
        try
        {
            var book = await _bookService.GetPopularAsync();
            return Ok(book);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.ErrorCode, new { errormessage = ex.Message });
        }
    }
    [Authorize(Roles = $"{UserRoles.User}")]
    [HttpGet("search")]
    public  ActionResult SearchAsync([FromQuery] SearchBookRequest request)
    {
        try
        {
            var books =  _bookService.SearchAsync(request);
            return Ok(books);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.ErrorCode, new { errormessage = ex.Message });
        }
    }
}