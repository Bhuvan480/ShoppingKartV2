using Microsoft.EntityFrameworkCore;
using ShoppingKart.Models;

namespace ShoppingKart.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            
        }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasNoKey().ToView("ProductModel");
        }
    }
}
