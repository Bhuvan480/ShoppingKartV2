using Microsoft.EntityFrameworkCore;
using ShoppingKart.Data;
using ShoppingKart.Models;
using ShoppingKart.Repository;

namespace ShoppingKart.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly AppDbContext db;

        public RegisterService(AppDbContext appDbContext)
        {
            db = appDbContext;
        }
        public int InsertUser(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("Invalid user data. Name, Email, and Password are required.");
            }

            try
            {
                db.Add(user);
                var Rows = 1;// db.Database.ExecuteSqlInterpolated($"SP_InsertUser {user.Name},{user.Email},{user.Password}");
                db.SaveChanges();
                return Rows;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("duplicate") || e.InnerException.Message.Contains("duplicate"))
                    return 0;
                else
                    throw;
            }
        }
    }
}
