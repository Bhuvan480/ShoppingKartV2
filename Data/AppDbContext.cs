using Microsoft.EntityFrameworkCore;
using ShoppingKart.Models;

namespace ShoppingKart.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(e => e.Email).IsUnique();
            //modelBuilder.Entity<UserModel>(
            //    entity =>
            //    {
            //        entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();
            //    });
        }

    }
}
