using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        // ссылка на контекст
        private readonly AppDBContext _context;

        // Метод-конструктор для инициализации
        public PostRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreatePost(Post post)
        {
            post.postCreateDate = DateTime.Now.ToString();

            // Добавление статьи
            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
                await _context.Post.AddAsync(post);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task DelPost(Post post)
        {
            // Удаление статьи
            var dbpost = _context.Post.Where(u => u.id == post.id).First();
            if (dbpost != null)
                _context.Post.Remove(dbpost);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task EditPost(Post post,int id)
        {
            // редактирование статьи

            var dbpost = _context.Post.Where(u => u.id == id).First();
            if (dbpost != null)
            {
                dbpost.postName = post.postName;
                dbpost.postText = post.postText;
                dbpost.Tags = post.Tags;
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();

        }

        public async Task<Post[]> GetPosts()
        {
            // Получим все статьи
            return await _context.Post.Include(p => p.Tags).Include(u => u.User).ToArrayAsync();
        }

        // Получим все статьи автора по его id
        public async Task<Post[]> GetPostsByUserId(string id)
        {
            var postsByUserId = await _context.Post.Include(p => p.User).Where(p => p.User.Id ==id).ToArrayAsync();

            return postsByUserId;
        }

        // найти конкретную статью по Id
        public async Task<Post?> GetPostById(int id)
        {
            var postById = await _context.Post.Include(u =>u.User).Include(c => c.Comments).ThenInclude(cu =>cu.User).Where(p => p.id == id).FirstOrDefaultAsync();

            return postById;
        }
    }
}
