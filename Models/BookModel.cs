namespace BookStore.Models
{
    // here we define the book model as property of the bookmodel class
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public int TotalPages { get; set; }
    }
}
