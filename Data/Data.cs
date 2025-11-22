using Models;
public class Data
{
    private List<Book> books = new()
    {
        new Book
        {
            Id = 1,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
        },
        new Book
        {
            Id = 2,
            Title = "To Kill a Mockingbird",
            Author = "Jiru Gutema",
        },
    };

    public List<Book> GetBooks(){
      return books;
    }
}
