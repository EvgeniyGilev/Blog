using BlogWebApp.BLL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogWebApp.DAL.Context
{
    /// <summary>
    /// DBContext.
    /// </summary>
    public class AppDBContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Объекты таблицы Users.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Объекты таблицы Post.
        /// </summary>
        public DbSet<Post> Post { get; set; }

        /// <summary>
        /// Объекты таблицы Role.
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        /// Объекты таблицы Comment.
        /// </summary>
        public DbSet<Comment> Comment { get; set; }

        /// <summary>
        /// Объекты таблицы Comment.
        /// </summary>
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
        /// Ons the configuring.
        /// </summary>
        /// <param name="options">The options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source = DAL/DB/blog.db");

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
