// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;

namespace BlogAPI.DATA.Repositories.Interfaces
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
        /// Delete the comment.
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
