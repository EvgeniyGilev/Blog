using BlogWebApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Context
{
    public class AppDBContext : DbContext
    {
        // Объекты таблицы Users
        public DbSet<UserEntity> User { get; set; }

        // Объекты таблицы Post
        public DbSet<PostEntity> Post { get; set; }

        //Объекты таблицы Role
        public DbSet<RoleEntity> Role { get; set; }

        //Объекты таблицы Comment
        public DbSet<CommentEntity> Comment { get; set; }

        //Объекты таблицы Comment
        public DbSet<TagEntity> Tag { get; set; }

        public AppDBContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source = DAL/DB/blog.db; Version = 3");

    }
}
