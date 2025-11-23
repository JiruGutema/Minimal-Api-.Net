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
        return Results.Ok("The servier is up and running!");
    }
);
app.MapGet(
    "books",
    () =>
    {
        Console.WriteLine("Fetching all books");
        if (books.Count == 0)
        {
            return Results.NotFound("No books available now!");
        }

        return Results.Ok(books);
    }
);

app.MapGet(
    "/books/{id}",
    (int id) =>
    {
        Console.WriteLine("Retriving book with id: ", id);
        var book = books.Find(b => b.Id == id);
        if (book is null)
        {
            return Results.NotFound("Sorry, There server couldn't be able to find the book!");
        }

        return Results.Ok(book);
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
        return Results.Ok(books);
    }
);
app.MapDelete(
    "/books/{id}",
    (int id) =>
    {
        // firt find the existance of the book specified.
        var bookToBeDeleted = books.Find(book => book.Id == id);
        // return 404 if not found
        if (bookToBeDeleted is null)
        {
            return Results.NotFound("The book not found!");
        }
        else
        {
            var deleted = books.Remove(bookToBeDeleted);
            Console.WriteLine(deleted);
            if (!deleted)
            {
                return Results.InternalServerError("Unable to delete the book");
            }
            return Results.Ok(books);
        }
    }
);

app.UseHttpsRedirection();
app.Run();
