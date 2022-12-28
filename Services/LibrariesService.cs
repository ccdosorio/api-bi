
using api_bi.Models;
using Microsoft.EntityFrameworkCore;

namespace api_bi.Services;

public class LibrariesService : ILibrariesService
{

    BiContext _context;

    public LibrariesService(BiContext context)
    {
        _context = context;
    }

    public IEnumerable<Library> Get()
    {
        return _context.Libraries;
    }

    public Library GetById(int libraryId)
    {
        return _context.Libraries.Find(libraryId);
    }

    public IEnumerable<Book> GetBooks(int libraryId)
    {
        var currentLibrary = _context.Libraries.Find(libraryId);

        if (currentLibrary == null)
        {
            return Enumerable.Empty<Book>();
        }

        return _context.Books.FromSql($"SELECT * FROM book b WHERE b.libraryid = {libraryId}").ToList();

    }

    public async Task Save(Library library)
    {
        _context.Add(library);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> SaveBookToLibrary(int libraryId, Book book)
    {
        var currentLibrary = _context.Libraries.Find(libraryId);

        if (currentLibrary != null)
        {
            book.createdAt = DateTime.Now;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task Update(int id, Library library)
    {
        var currentLibrary = _context.Libraries.Find(id);

        if (currentLibrary != null)
        {
            currentLibrary.name = library.name;
            currentLibrary.location = library.location;

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Delete(int id)
    {
        var currentLibrary = _context.Libraries.Find(id);

        if (currentLibrary == null)
        {
            return false;
        }

        _context.Remove(currentLibrary);

        await _context.SaveChangesAsync();

        return true;
    }
}

public interface ILibrariesService
{
    IEnumerable<Library> Get();
    Library GetById(int libraryId);
    IEnumerable<Book> GetBooks(int libraryId);
    Task Save(Library library);
    Task<bool> SaveBookToLibrary(int libraryId, Book book);
    Task Update(int id, Library library);
    Task<bool> Delete(int id);
}