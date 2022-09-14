using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Comments
{
    /// <summary>
    /// The create comment model.
    /// </summary>
    public class CreateCommentModel
    {
        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        [Required(ErrorMessage = "Id статьи, к которой добавляется комментарий")]
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        [Required(ErrorMessage = "Текст комментария должен быть не пустым")]
        public string CommentText { get; set; }
    }
}
