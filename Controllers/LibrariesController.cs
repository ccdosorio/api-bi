using api_bi.Models;
using api_bi.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_bi.Controllers;

[Route("api/[controller]")]
public class LibrariesController : ControllerBase
{
    ILibrariesService _librariesService;

    public LibrariesController(ILibrariesService librariesService)
    {
        _librariesService = librariesService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_librariesService.Get());
    }

    [HttpGet("{libraryId}")]
    public ActionResult<Library> GetById(int libraryId)
    {
        var library = _librariesService.GetById(libraryId);
        if (library is null)
            return NotFound();
            
        return library;
    }

    [HttpGet("{libraryId}/books")]
    public IActionResult GetBooks(int libraryId)
    {
        var result = _librariesService.GetBooks(libraryId);

        if (result.Count() == 0)
        {
            return StatusCode(404);
        }

        return Ok(result);
    }

    [HttpPost("{libraryId}/books")]
    public async Task<IActionResult> PostBook(int libraryId, [FromBody] Book book)
    {
        var result = await _librariesService.SaveBookToLibrary(libraryId, book);

        if (!result)
        {
            return StatusCode(404);
        }
        return StatusCode(201);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Library library)
    {
        _librariesService.Save(library);
        return StatusCode(201);
    }

    [HttpPut("{libraryId}")]
    public IActionResult Put(int libraryId, [FromBody] Library library)
    {
        _librariesService.Update(libraryId, library);
        return Ok();
    }

    [HttpDelete("{libraryId}")]
    public async Task<IActionResult> Delete(int libraryId)
    {
        var result = await _librariesService.Delete(libraryId);

        if (!result)
        {
            return StatusCode(404);
        }
        return StatusCode(204);
    }
}