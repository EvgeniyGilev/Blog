using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Posts
{
    /// <summary>
    /// The create post model.
    /// </summary>
    public class CreatePostModel
    {
        /// <summary>
        /// Gets or sets the post name.
        /// </summary>
        [Required(ErrorMessage = "Название Статьи должно быть заполнено")]
        public string PostName { get; set; }

        /// <summary>
        /// Gets or sets the post text.
        /// </summary>
        [Required(ErrorMessage = "Содержание Статьи должно быть заполнено")]
        public string PostText { get; set; }
    }
}