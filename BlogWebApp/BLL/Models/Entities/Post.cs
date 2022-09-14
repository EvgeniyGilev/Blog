namespace BlogWebApp.BLL.Models.Entities
{
    /// <summary>
    /// The post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the post name.
        /// </summary>
        public string postName { get; set; }

        /// <summary>
        /// Gets or sets the post text.
        /// </summary>
        public string postText { get; set; }

        /// <summary>
        /// Gets or sets the post create date.
        /// </summary>
        public string? postCreateDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// ссылка на пользователя.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// ссылка на теги.
        /// </summary>
        public virtual List<Tag> Tags { get; set; } = new ();

        /// <summary>
        /// Gets or sets the comments.
        /// ссылка на комментарии.
        /// </summary>
        public virtual List<Comment> Comments { get; set; } = new ();
    }
}
