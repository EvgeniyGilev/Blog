// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlogAPI.DATA.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogAPI.DATA.Context
{
    public sealed class AppDbContext : IdentityDbContext<User>
    {
        // Объекты таблицы Users
        public DbSet<User>? User { get; set; }

        // Объекты таблицы Post
        public DbSet<Post>? Post { get; set; }

        // Объекты таблицы Role
        public DbSet<Role>? Role { get; set; }

        // Объекты таблицы Comment
        public DbSet<Comment>? Comment { get; set; }

        // Объекты таблицы Comment
        public DbSet<Tag>? Tag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
