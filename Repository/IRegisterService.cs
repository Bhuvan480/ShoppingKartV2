using ShoppingKart.Models;

namespace ShoppingKart.Repository
{
    public interface IRegisterService
    {
        int InsertUser(UserModel user);
    }
}
