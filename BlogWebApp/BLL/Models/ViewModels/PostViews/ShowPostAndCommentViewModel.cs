using BlogWebApp.BLL.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{
    /// <summary>
    /// The show post and comment view model.
    /// </summary>
    public class ShowPostAndCommentViewModel
    {
        /// <summary>
        /// Gets or sets the show post.
        /// </summary>
        public Post? ShowPost { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [Required(ErrorMessage = "Комментарий не может быть пустым")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        public int PostId { get; set; }
    }
}
