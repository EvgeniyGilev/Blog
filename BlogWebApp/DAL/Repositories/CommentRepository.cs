using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        // ссылка на контекст
        private readonly AppDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CommentRepository(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public async Task CreateComment(Comment comment)
        {
            comment.commentCreatedDate = DateTime.Now.ToString();

            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
            {
                await _context.Comment.AddAsync(comment);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Del the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public async Task DelComment(Comment comment)
        {
            // Удаление комментария
            var dbcomment = _context.Comment.Where(u => u.id == comment.id).First();
            if (dbcomment != null)
            {
                _context.Comment.Remove(dbcomment);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public async Task EditComment(Comment comment)
        {
            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
            {
                _context.Comment.Update(comment);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the comment by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task<Comment?> GetCommentById(int id)
        {
            var commentById = await _context.Comment.FindAsync(id);

            return commentById;
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<Comment[]> GetComments()
        {
            // Получим все статьи
            return await _context.Comment.ToArrayAsync();
        }
    }
}
