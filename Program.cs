using Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
Data data = new Data();
List<Book> books = data.GetBooks();

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
    (Book book) =>
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
        return books;
    }
);

app.UseHttpsRedirection();
app.Run();
