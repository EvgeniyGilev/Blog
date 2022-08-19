using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Entities;
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

        public async Task CreatePost(PostEntity post)
        {
            post.postCreateDate = DateTime.Now.ToString();

            // Добавление статьи
            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
                await _context.Post.AddAsync(post);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task DelPost(PostEntity post)
        {
            // Удаление статьи
            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
                _context.Post.Remove(post);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task EditPost(PostEntity post)
        {
            // редактирование статьи
            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
                _context.Post.Update(post);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task<PostEntity[]> GetPosts()
        {
            // Получим все статьи
            return await _context.Post.ToArrayAsync();
        }

        // Получим все статьи автора по его id
        public async Task<PostEntity[]> GetPostsByUserId(int id)
        {
            var postsByUserId = await _context.Post.Where(p => p.userId ==id).ToArrayAsync();

            return postsByUserId;
        }

        // найти конкретную статью по Id
        public async Task<PostEntity?> GetPostById(int id)
        {
            var postById = await _context.Post.FindAsync(id);

            return postById;
        }
    }
}
