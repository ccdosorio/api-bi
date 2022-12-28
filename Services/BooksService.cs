using api_bi.Models;

namespace api_bi.Services;

public class BooksService : IBooksService
{
    BiContext _context;

    public BooksService(BiContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> Get()
    {
        return _context.Books;
    }

    public async Task Update(int id, Book book)
    {
        var currentBook = _context.Books.Find(id);

        if (currentBook != null)
        {
            currentBook.name = book.name;
            currentBook.category = book.category;

            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var currentBook = _context.Books.Find(id);

        if (currentBook != null)
        {
            _context.Remove(currentBook);

            await _context.SaveChangesAsync();
        }
    }
}

public interface IBooksService
{
    IEnumerable<Book> Get();
    Task Update(int id, Book book);
    Task Delete(int id);
}