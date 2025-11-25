using Models;
using Microsoft.EntityFrameworkCore;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions options) : base(options) { }
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<User> users {get; set;} = null!;
}
