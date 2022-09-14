using BlogWebApp.BLL.Models.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    /// <summary>
    /// The comment repository.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        Task CreateComment(Comment comment);

        /// <summary>
        /// Edits the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        Task EditComment(Comment comment);

        /// <summary>
        /// Del the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        Task DelComment(Comment comment);

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<Comment[]> GetComments();

        /// <summary>
        /// Gets the comment by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task<Comment?> GetCommentById(int id);
    }
}
