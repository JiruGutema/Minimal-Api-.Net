using Models;

namespace EndPoints
{
    public class UserEndPoints
    {
        private Data data = new Data();
        private List<User> users;

        public UserEndPoints()
        {
            users = data.GetUsers();
        }

        public IResult GetUserById(int id)
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

        public IResult GetAllUsers()
        {
            return Results.Ok(users);
        }

        public IResult Login(Login Req)
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

        public IResult Signup(Singup Req)
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
    }
}
