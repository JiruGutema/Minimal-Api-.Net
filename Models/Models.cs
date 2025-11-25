namespace Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public required string name { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
    }

}
