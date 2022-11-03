// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;

namespace BlogWebApp.BLL.Interfaces.Services
{
    /// <summary>
    /// The comment service.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// получить комментарий по его id.
        /// </summary>
        /// <param name="id">id комментария.</param>
        /// <returns>comment.</returns>
        Task<Comment?> GetCommentById(int id);

        /// <summary>
        /// Создание комментария.
        /// </summary>
        /// <param name="newComment">Новый комментарий.</param>
        /// <returns>Boolean - true - success or false - failure.</returns>
        Task<bool> CreateComment(Comment newComment);

        /// <summary>
        /// Удаление комментария.
        /// </summary>
        /// <param name="comment">комментарий, который будет удален.</param>
        /// <returns>Boolean - true - success or false - failure..</returns>
        Task<bool> DeleteComment(Comment comment);
    }
}
