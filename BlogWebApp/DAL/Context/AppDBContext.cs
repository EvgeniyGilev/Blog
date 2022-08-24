using BlogWebApp.BLL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Context
{
    public class AppDBContext : DbContext
    {
        // Объекты таблицы Users
        public DbSet<User> User { get; set; }

        // Объекты таблицы Post
        public DbSet<Post> Post { get; set; }

        //Объекты таблицы Role
        public DbSet<Role> Role { get; set; }

        //Объекты таблицы Comment
        public DbSet<Comment> Comment { get; set; }

        //Объекты таблицы Comment
        public DbSet<Tag> Tag { get; set; }

        public AppDBContext() 
        {
            //Database.EnsureDeleted();

            Database.EnsureCreated();

            

        }


        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source = DAL/DB/blog.db");
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Room>().ToTable("Rooms");
            //builder.Entity<Device>().ToTable("Devices");
        }
    }
}
