var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

List<Book> books = new()
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

app.MapGet(
    "/",
    () =>
    {
        return "OK";
    }
);
app.MapGet(
    "books",
    () =>
    {
        Console.WriteLine("Fetching all books");
        return books;
    }
);

app.MapGet(
    "/books/{id}",
    (int id) =>
    {
        Console.WriteLine($"returning books with id: {id}");
        var book = books.Where(b => b.Id == id);
        return book;
    }
);

app.MapPost(
    "/books",
    (int id, string title, string Author) =>
    {
        Console.WriteLine($"Creating book with id: {id}, title: {title}, author: {Author}");

        Book newBook = new()
        {
            Id = id,
            Title = title,
            Author = Author,
        };
        books.Add(newBook);
        return books;
    }
);
app.UseHttpsRedirection();
app.Run();

class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}
