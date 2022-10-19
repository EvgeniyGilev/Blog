// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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