using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Posts
{
    /// <summary>
    /// The get post by id model.
    /// </summary>
    public class GetPostByIdModel
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
        /// Gets or sets the post text.
        /// </summary>
        public string PostText { get; set; }

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