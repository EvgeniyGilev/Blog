// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Context;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DATA.Repositories
{
    /// <summary>
    /// The comment repository.
    /// </summary>
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CommentRepository(AppDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public async Task CreateComment(Comment comment)
        {
            comment.CommentCreatedDate = DateTime.Now.ToString();

            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
            {
                await _context.Comment.AddAsync(comment);
            }
        }

        /// <summary>
        /// Del the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public async Task DelComment(Comment comment)
        {
            // Удаление комментария
            var dbcomment = _context.Comment.Where(u => u.Id == comment.Id).First();
            if (dbcomment != null)
            {
                _context.Comment.Remove(dbcomment);
            }
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
