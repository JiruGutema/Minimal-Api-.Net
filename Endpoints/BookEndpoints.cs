using Models;

namespace EndPoints
{
    public class BookEndpoints
    {
        private Data data = new Data();
        private List<Book> books;

        public BookEndpoints()
        {
            books = data.GetBooks();
        }

        public IResult GetAllBooks()
        {
            Console.WriteLine("Fetching all books");
            if (books.Count == 0)
            {
                return Results.NotFound("No books available now!");
            }
            return Results.Ok(books);
        }

        public IResult GetBookById(int id)
        {
            Console.WriteLine($"Retrieving book with id: {id}");
            var book = books.Find(b => b.Id == id);
            if (book is null)
            {
                return Results.NotFound("Sorry, the server couldn't find the book!");
            }
            return Results.Ok(book);
        }

        public IResult CreateBook(Book book)
        {
            int id = book.Id;
            string title = book.Title;
            string author = book.Author;
            Console.WriteLine($"Creating book with id: {id}, title: {title}, author: {author}");

            Book newBook = new()
            {
                Id = id,
                Title = title,
                Author = author,
            };
            books.Add(newBook);
            return Results.Ok(books);
        }

        public IResult DeleteBook(int id)
        {
            var bookToBeDeleted = books.Find(book => book.Id == id);
            if (bookToBeDeleted is null)
            {
                return Results.NotFound("The book not found!");
            }
            var deleted = books.Remove(bookToBeDeleted);
            Console.WriteLine(deleted);
            if (!deleted)
            {
                return Results.StatusCode(500);
            }
            return Results.Ok(books);
        }
    }
}
