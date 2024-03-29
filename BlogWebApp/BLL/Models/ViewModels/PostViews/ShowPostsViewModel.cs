﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{
    /// <summary>
    /// The show posts view model.
    /// </summary>
    public class ShowPostsViewModel
    {
        /// <summary>
        /// Gets or sets the show posts.
        /// </summary>
        public List<Post>? ShowPosts { get; set; }

        /// <summary>
        /// Minis the post.
        /// </summary>
        /// <param name="miniPost">The mini post.</param>
        /// <returns>A string.</returns>
        public string? MiniPost(Post miniPost)
        {
            if (miniPost.PostText is { Length: > 300 })
            {
                return miniPost.PostText.Substring(0, 300) + " ....";
            }

            return miniPost.PostText;
        }
    }
}