﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Context;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;

namespace Blog.MockData.Repositories
{
    /// <summary>
    /// The comment repository.
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public Task CreateComment(Comment comment) => Task.FromResult(0);


        /// <summary>
        /// Del the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public Task DelComment(Comment comment) => Task.FromResult(0);

        /// <summary>
        /// Edit the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>A Task.</returns>
        public Task EditComment(Comment comment) => Task.FromResult(0);

        /// <summary>
        /// Gets the comment by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public Task<Comment> GetCommentById(int id)
         => new Comment
         {
             Id = id,
             CommentCreatedDate = DateTime.Now.ToString(),
             CommentTexte = "Комментарий",
         }.AsTask();

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <returns>A Task.</returns>
        public Task<Comment[]> GetComments()
        {
            Comment[] ArrComment = new Comment[1];
            ArrComment[0] = new Comment
            {
                Id = 1,
                CommentCreatedDate = DateTime.Now.ToString(),
                CommentTexte = "Комментарий",
                Post = new Post()
                {
                    Id = 1,
                    PostName = "Статья",
                    PostText = "Текст статьи",
                    PostCreateDate = DateTime.Now.ToString(),
                },
                User = new User()
                {
                    Email = "test@gmail.com",
                    UserFirstName = "TestFirstName",
                    UserLastName = "TestLastName",
                }
            };
            return ArrComment.AsTask();
        }
    }
}
