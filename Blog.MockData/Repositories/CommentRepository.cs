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
             id = id,
             commentCreatedDate = DateTime.Now.ToString(),
             commentTexte = "Комментарий",
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
                id = 1,
                commentCreatedDate = DateTime.Now.ToString(),
                commentTexte = "Комментарий",
                Post = new Post()
                {
                    id = 1,
                    postName = "Статья",
                    postText = "Текст статьи",
                    postCreateDate = DateTime.Now.ToString(),
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
