using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using NewsModule.Models;

namespace NewsModule.Data
{
    public class NewsModuleContext: IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public NewsModuleContext(DbContextOptions<NewsModuleContext> options) : base(options)
        {
            //Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1,publishTime=DateTime.Now, Title = "Title1", Annotation = "Annotation 1",Description = "Description 1" },
                new Article { Id = 2, publishTime = DateTime.Now, Title = "Title2", Annotation = "Annotation 2", Description = "Description 2" }
                );

            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "User1", Email = "Email@email.com", PasswordHash = "12345", Role = enumRoles.User});
        }
           
    }
}
