// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.DATA.Models
{
    /// <summary>
    /// The post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the post name.
        /// </summary>
        public string? PostName { get; set; }

        /// <summary>
        /// Gets or sets the post text.
        /// </summary>
        public string? PostText { get; set; }

        /// <summary>
        /// Gets or sets the post create date.
        /// </summary>
        public string? PostCreateDate { get; set; }


        /// <summary>
        /// Gets or sets the user.
        /// ссылка на пользователя.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// ссылка на теги.
        /// </summary>
        public virtual List<Tag> Tags { get; set; } = new();

        /// <summary>
        /// Gets or sets the comments.
        /// ссылка на комментарии.
        /// </summary>
        public virtual List<Comment> Comments { get; set; } = new();
    }
}
