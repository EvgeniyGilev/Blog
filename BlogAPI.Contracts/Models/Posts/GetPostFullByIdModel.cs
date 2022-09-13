using BlogAPI.Contracts.Models.Tags;
using BlogAPI.Contracts.Models.Comments;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Posts
{
    /// <summary>
    /// The get post full data by id model.
    /// </summary>
    public class GetPostFullByIdModel
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

        /// <summary>
        /// Gets or sets the tags.
        /// ссылка на теги.
        /// </summary>
        public virtual List<TagView> Tags { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// ссылка на комментарии.
        /// </summary>
        public virtual List<CommentView> Comments { get; set; }
    }
}