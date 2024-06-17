using Microsoft.EntityFrameworkCore;
using NewsModule.Models;

namespace NewsModule.Data
{
    public class NewsModuleContext:DbContext
    {
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public NewsModuleContext()
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").SetBasePath(Directory.GetCurrentDirectory()).Build();
            
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1,publishTime=DateTime.Now, Title = "Title1", Annotation = "Annotation 1",Description = "Description 1" },
                new Article { Id = 2, publishTime = DateTime.Now, Title = "Title2", Annotation = "Annotation 2", Description = "Description 2" }
                );

            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "User1", Email = "Email@email.com", PasswordHash = "12345", Role = "user"});
        }
           
    }
}
