// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{
    /// <summary>
    /// The create post view model.
    /// </summary>
    public class CreatePostViewModel
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

        /// <summary>
        /// Gets or sets the post author email.
        /// </summary>
        [Required(ErrorMessage = "Для публикации статьи надо залогиниться")]
        public string PostAuthorEmail { get; set; }

        /// <summary>
        /// Gets or sets the post tags.
        /// </summary>
        public List<Tag>? PostTags { get; set; }
    }
}
