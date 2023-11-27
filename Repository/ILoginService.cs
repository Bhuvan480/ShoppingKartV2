using ShoppingKart.Models;

namespace ShoppingKart.Repository
{
    public interface ILoginService
    {
        UserModel FindUserByEmail(string email);

        bool ValidateUser(UserModel user, string password);
    }
}