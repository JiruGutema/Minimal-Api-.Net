using System.Threading.Tasks;
using DTO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EndPoints
{
    public class UserEndPoints
    {
        private readonly AppDb _db;

        public UserEndPoints(AppDb db)
        {
            _db = db;
        }

        public async Task<IResult> GetUserById(int id)
        {
            var user = await _db.users.FindAsync(id);
            if (user is null)
            {
                return Results.NotFound("User not found!");
            }
            else
            {
                return Results.Ok(user);
            }
        }

        public async Task<IResult> GetAllUsers()
        {
            return Results.Ok(await _db.users.ToListAsync());
        }

        public async Task<IResult> Login(Login Req)
        {
            string email = Req.email;
            string password = Req.password;
            var user = await _db.users.FirstOrDefaultAsync(u => u.email == email);
            if (user is null)
            {
                return Results.NotFound("User not found");
            }

            var check = _db.users.FirstOrDefaultAsync(u =>
                u.email == email && u.password == password
            );
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

        public async Task<IResult> Signup(Singup Req)
        {
            string email = Req.email;
            string password = Req.password;
            string name = Req.name;
            int id = _db.users.Count() + 1;
            var checkId = _db.users.FirstOrDefaultAsync(user => user.Id == id);
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

            _db.users.Add(user);
            await _db.SaveChangesAsync();

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
