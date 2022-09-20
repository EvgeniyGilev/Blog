﻿using BlogAPI.DATA.Models;

namespace BlogAPI.Interfaces.Services
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
        Task<Comment> GetCommentById(int id);

        /// <summary>
        /// Создание комментария.
        /// </summary>
        /// <param name="newComment">Новый комментарий.</param>
        /// <returns>Boolean - true - success or false - failure.</returns>
        Task<bool> CreateComment(Comment newComment);

        /// <summary>
        /// Удаление комментария.
        /// </summary>
        /// <param name="Comment">комментарий, который будет удален.</param>
        /// <returns>Boolean - true - success or false - failure..</returns>
        Task<bool> DeleteComment(Comment Comment);
    }
}
