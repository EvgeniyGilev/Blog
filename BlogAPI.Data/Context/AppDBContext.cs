using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlogAPI.DATA.Models;

namespace BlogAPI.DATA.Context
{
    public class AppDBContext : IdentityDbContext<User>
    {
        // Объекты таблицы Users
        public DbSet<User> User { get; set; }

        // Объекты таблицы Post
        public DbSet<Post> Post { get; set; }

        // Объекты таблицы Role
        public DbSet<Role> Role { get; set; }

        // Объекты таблицы Comment
        public DbSet<Comment> Comment { get; set; }

        // Объекты таблицы Comment
        public DbSet<Tag> Tag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDBContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <summary>
        /// Ons the model creating.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
