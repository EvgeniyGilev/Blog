// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;

namespace BlogAPI.DATA.Repositories.Interfaces
{
    /// <summary>
    /// The post repository.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>A Task.</returns>
        Task CreatePost(Post post);

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task EditPost(Post post, int id);

        /// <summary>
        /// Delete the post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>A Task.</returns>
        Task DelPost(Post post);

        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<Post[]> GetPosts();

        /// <summary>
        /// Gets the posts by user id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task<Post[]> GetPostsByUserId(string id);

        /// <summary>
        /// Gets the post by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task<Post?> GetPostById(int id);
    }
}
