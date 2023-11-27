using ShoppingKart.Data;
using ShoppingKart.Models;
using ShoppingKart.Repository;

namespace ShoppingKart.Service
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext db;

        public LoginService(AppDbContext appDb)
        {
            db = appDb;
        }
        public UserModel FindUserByEmail(string email)
        {
            UserModel user = db.Users.FirstOrDefault(x => x.Email == email);

            return user;
        }

        public bool ValidateUser(UserModel user, string password)
        {
             string hasedpassword = BCrypt.Net.BCrypt.HashPassword(user.Password);


            if (BCrypt.Net.BCrypt.Verify(password, hasedpassword))
            {
                // Passwords match, so authentication is successful
                // You can proceed with logging the user in
                return true;
            }

            return false;
        }
    }
}
