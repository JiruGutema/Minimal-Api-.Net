using Microsoft.EntityFrameworkCore;
//import a scope here 
using Models;
using DTO;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDb>(options =>
    options.UseSqlite("Data Source=app.db"));

//App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

Data data = new Data();
var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDb>();

EndPoints.BookEndpoints bookEndpoints = new EndPoints.BookEndpoints(db);
EndPoints.UserEndPoints userEndPoints = new EndPoints.UserEndPoints(db);
EndPoints.HealthEndpoints healthEndpoints = new EndPoints.HealthEndpoints();
// endpoints
app.MapGet("/", healthEndpoints.GetRootEndpoint);
app.MapGet("/api/books", bookEndpoints.GetAllBooks);
app.MapGet("/api/books/{id}", (int id) => bookEndpoints.GetBookById(id));
app.MapPost("/api/books", (Book book) => bookEndpoints.CreateBook(book));
app.MapDelete("/api/books/{id}", (int id) => bookEndpoints.DeleteBook(id));
app.MapGet("/api/user/{id}", (int id) => userEndPoints.GetUserById(id));
app.MapGet("/api/users", userEndPoints.GetAllUsers);
app.MapPost("/api/login", (Login Req) => userEndPoints.Login(Req));
app.MapPost("/api/signup", (Singup Req) => userEndPoints.Signup(Req));
app.UseHttpsRedirection();
app.Run();

