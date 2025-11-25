using Microsoft.EntityFrameworkCore;
using Models;

namespace EndPoints
{
    public class BookEndpoints
    {
        private readonly AppDb _db;

        public BookEndpoints(AppDb db)
        {
            _db = db;
        }

        public async Task<IResult> GetAllBooks()
        {
            Console.WriteLine("Fetching all books");
            List<Book> books = await _db.Books.ToListAsync();
            if (books.Count == 0)
            {
                return Results.NotFound("No books available now!");
            }
            return Results.Ok(books);
        }

        public async Task<IResult> GetBookById(int id)
        {
            Console.WriteLine($"Retrieving book with id: {id}");

            Book? book = await _db.Books.FindAsync(id);
            if (book is null)
            {
                return Results.NotFound("Sorry, the server couldn't find the book!");
            }
            return Results.Ok(book);
        }

        public async Task<IResult> CreateBook(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return Results.Ok(book);
        }

        public async Task<IResult> DeleteBook(int id)
        {
            var bookToBeDeleted = await _db.Books.FindAsync(id);
            if (bookToBeDeleted is null)
            {
                return Results.NotFound("The book not found!");
            }
            _db.Books.Remove(bookToBeDeleted);
            await _db.SaveChangesAsync();
            return Results.Ok();
        }
    }
}
