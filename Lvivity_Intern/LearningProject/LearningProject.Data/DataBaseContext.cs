using LearningProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningProject.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
           Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role[] { new Role { Id = 1, Name = "admin" }, new Role { Id = 2, Name = "user" } });
            modelBuilder.Entity<User>().HasData(new User[] { new User { Id = 2, UserName = "admin", Password = "admin1", RoleId = 1 },
                new User { Id = 3, UserName = "volodya", Password = "volodya1", RoleId = 2 } });
            modelBuilder.Entity<Post>().HasData(new Post[] {  new Post
            {
                Id = 5,
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
                "in favor of a unique environment that best meets the needs of the customer.",
                ImageUrl = "images.jpg"
            }, new Post
            {
                Id = 7,
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
                "in favor of a unique environment that best meets the needs of the customer.",
                ImageUrl = "images.jpg"
            }});
        }
    }
}
