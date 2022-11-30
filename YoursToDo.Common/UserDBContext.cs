using Microsoft.EntityFrameworkCore;
using YoursToDo.Common.Entity_Configuration;
using YoursToDo.Common.Models;

namespace YoursToDo.Common
{
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DBYours.db;integrated security=True;TrustServerCertificate=True");
            dbContextOptionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}