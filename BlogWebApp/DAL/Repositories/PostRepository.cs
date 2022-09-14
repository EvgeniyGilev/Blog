using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Repositories
{
    /// <summary>
    /// The post repository.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        // ссылка на контекст
        private readonly AppDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PostRepository(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>A Task.</returns>
        public async Task CreatePost(Post post)
        {
            post.postCreateDate = DateTime.Now.ToString();

            // Добавление статьи
            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
            {
                await _context.Post.AddAsync(post);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Del the post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>A Task.</returns>
        public async Task DelPost(Post post)
        {
            // Удаление статьи
            var dbpost = _context.Post.Where(u => u.id == post.id).First();
            if (dbpost != null)
            {
                _context.Post.Remove(dbpost);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task EditPost(Post post,int id)
        {
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

        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<Post[]> GetPosts()
        {
            // Получим все статьи
            return await _context.Post.Include(p => p.Tags).Include(u => u.User).ToArrayAsync();
        }

        /// <summary>
        /// Gets the posts by user id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task<Post[]> GetPostsByUserId(string id)
        {
            var postsByUserId = await _context.Post.Include(p => p.User).Where(p => p.User.Id ==id).ToArrayAsync();

            return postsByUserId;
        }

        /// <summary>
        /// Gets the post by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task<Post?> GetPostById(int id)
        {
            var postById = await _context.Post.Include(t => t.Tags).Include(u => u.User).Include(c => c.Comments).ThenInclude(cu => cu.User).Where(p => p.id == id).FirstOrDefaultAsync();

            return postById;
        }
    }
}
