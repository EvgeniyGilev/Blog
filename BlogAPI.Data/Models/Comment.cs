// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string? CommentTexte { get; set; }

        /// <summary>
        /// Gets or sets the comment created date.
        /// </summary>
        public string? CommentCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the post.
        /// ссылка на статью.
        /// </summary>
        public virtual Post Post { get; set; } = new Post();

        /// <summary>
        /// Gets or sets the user.
        /// ссылка на пользователя.
        /// </summary>
        public virtual User? User { get; set; }
    }
}
