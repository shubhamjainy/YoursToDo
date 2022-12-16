using Microsoft.EntityFrameworkCore;
using YoursToDo.Common.Entity;
using YoursToDo.Common.Entity_Configuration;

namespace YoursToDo.Common
{
    public sealed class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        public UserDBContext()
        {
            // to get up and running
            this.Database.EnsureCreated();

            // load the entities into EF Core
            this.Users!.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DBYours.db;integrated security=True;TrustServerCertificate=True");
            dbContextOptionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }
    }
}