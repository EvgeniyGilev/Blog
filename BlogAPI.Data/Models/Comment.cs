namespace BlogAPI.DATA.Models
{
    /// <summary>
    /// The comment.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string commentTexte { get; set; }

        /// <summary>
        /// Gets or sets the comment created date.
        /// </summary>
        public string commentCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the post.
        /// ссылка на статью.
        /// </summary>
        public virtual Post Post { get; set; } = new();

        /// <summary>
        /// Gets or sets the user.
        /// ссылка на пользователя.
        /// </summary>
        public virtual User User { get; set; }
    }
}
