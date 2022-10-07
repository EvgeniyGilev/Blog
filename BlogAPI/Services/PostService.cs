﻿using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Interfaces.Services;
using Microsoft.Extensions.Hosting;

namespace BlogAPI.Services
{
    /// <summary>
    /// The post service.
    /// </summary>
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="postRepository"></param>
        /// <param name="unitOfWork"></param>
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates the post.
        /// </summary>
        /// <param name="newpost">The newpost.</param>
        /// <returns>номер новой статьи.</returns>
        async Task<int> IPostService.CreatePost(Post newpost)
        {
            await _postRepository.CreatePost(newpost);
            await _unitOfWork.CompleteAsync();

            // Если добавление прошло успешно получим id новой статьи
            var getpost = _postRepository.GetPosts().Result.FirstOrDefault(p => p.postName == newpost.postName).id;
            return getpost;
        }

        /// <summary>
        /// Deletes the post.
        /// </summary>
        /// <param name="id">номер статьи</param>
        /// <returns>true or false.</returns>
        async Task<bool> IPostService.DeletePost(int id)
        {
            Post? post = await _postRepository.GetPostById(id);

            if (post != null)
            {
                await _postRepository.DelPost(post);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="id">номер статьи.</param>
        /// <param name="newpost">данные для редактирования статьи.</param>
        /// <returns>true or false.</returns>
        async Task<bool> IPostService.EditPost(int id, Post newpost)
        {
            try
            {
                await _postRepository.EditPost(newpost, id);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the post by id.
        /// </summary>
        /// <param name="id">The id post.</param>
        /// <returns>Post.</returns>
        async Task<Post> IPostService.GetPostById(int id)
        {
            Post? post = await _postRepository.GetPostById(id);
            return post;
        }

        /// <summary>
        /// Get List posts.
        /// </summary>
        /// <returns>Lists the posts</returns>
        async Task<IEnumerable<Post>> IPostService.ListAsync()
        {
            return await _postRepository.GetPosts();
        }
    }
}
