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

    private List<User> users = new()
    {
        new User
        {
            Id = 1,
            name = "Jiru Gutema",
            password = "fakepassword",
            email = "jirudagutema@gmail.com",
        },
        new User
        {
            Id = 2,
            name = "Chala Gutema",
            password = "fakepassword",
            email = "chalagutema@gmail.com",
        },
    };

    public List<Book> GetBooks()
    {
        return books;
    }

    public List<User> GetUsers()
    {
        return users;
    }
}
