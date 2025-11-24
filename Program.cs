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
List<User> users = data.GetUsers();

app.MapGet(
    "/",
    () =>
    {
        return Results.Ok("The servier is up and running!");
    }
);
app.MapGet(
    "/api/books",
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
    "/api/books/{id}",
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
    "/api/books",
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
    "/api/books/{id}",
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
app.MapGet(
    "/api/user/{id}",
    (int id) =>
    {
        var user = users.Find(user => user.Id == id);
        if (user is null)
        {
            return Results.NotFound("User not found!");
        }
        else
        {
            return Results.Ok(user);
        }
    }
);

app.MapGet(
    "/api/users",
    () =>
    {
        return Results.Ok(users);
    }
);

app.MapPost(
    "api/login",
    (Login Req) =>
    {
        string email = Req.email;
        string password = Req.password;
        var checkPasswrod = users.Find(user => user.email == email);

        if (checkPasswrod is null)
        {
            return Results.NotFound("User not found");
        }

        var user = users.Find(user => user.email == email && user.password == password);
        if (user is null)
        {
            return Results.Unauthorized();
        }
        else
        {
            var response = new
            {
                message = "Login Successfull",
                token = "thisfaketoken",
                username = user.name,
                email = user.email,
            };
            return Results.Ok(response);
        }
    }
);

app.MapPost(
    "api/signup",
    (Singup Req) =>
    {
        string email = Req.email;
        string password = Req.password;
        string name = Req.name;
        int id = users.Count;

        var checkId = users.Find(user => user.Id == id);

        if (checkId is not null)
        {
            id += 1;
        }

        User user = new User
        {
            Id = id,
            name = name,
            email = email,
            password = password,
        };
        users.Add(user);

        var response = new
        {
            message = "Login Successfull",
            token = "thisfaketoken",
            username = user.name,
            Id = user.Id,
            email = user.email,
        };
        return Results.Ok(response);
    }
);

app.UseHttpsRedirection();
app.Run();
