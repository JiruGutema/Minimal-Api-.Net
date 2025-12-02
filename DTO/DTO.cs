namespace DTO
{ public class Login
    {
        public required string email { get; set; }
        public required string password { get; set; }
    }

    public class Singup
    {
        public required string email { get; set; }
        public required int Id { get; set; }
        public required string name { get; set; }
        public required string password { get; set; }
    }
}
