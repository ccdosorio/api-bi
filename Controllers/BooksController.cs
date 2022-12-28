using api_bi.Models;
using api_bi.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_bi.Controllers;

[Route("api/[controller]")]
public class BooksController : ControllerBase
{

    IBooksService _booksService;

    public BooksController(IBooksService bookService)
    {
        _booksService = bookService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_booksService.Get());
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Book book)
    {
        _booksService.Update(id, book);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _booksService.Delete(id);
        return Ok();
    }
}