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
        public List<Post> ShowPosts { get; set; }

        /// <summary>
        /// Minis the post.
        /// </summary>
        /// <param name="miniPost">The mini post.</param>
        /// <returns>A string.</returns>
        public string MiniPost(Post miniPost)
        {
            if (miniPost.postText.Length > 300)
            {
                return miniPost.postText.Substring(0, 300) + " ....";
            }
            else
            {
                return miniPost.postText;
            }
        }
    }
}