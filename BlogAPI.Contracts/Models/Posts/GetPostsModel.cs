// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.Contracts.Models.Posts
{
    /// <summary>
    /// The get posts model.
    /// </summary>
    public class GetPostsModel
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        public List<PostView> Posts { get; set; }

        /// <summary>
        /// The post view.
        /// </summary>
        public class PostView
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// Gets or sets the post title.
            /// </summary>
            public string PostTitle { get; set; }

            /// <summary>
            /// Gets or sets the author email.
            /// </summary>
            public string AuthorEmail { get; set; }

            /// <summary>
            /// Gets or sets the create date.
            /// </summary>
            public DateTime CreateDate { get; set; }
        }
    }
}