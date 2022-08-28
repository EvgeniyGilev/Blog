using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{

    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Название Статьи должно быть заполнено")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Содержание Статьи должно быть заполнено")]
        public string PostText { get; set; }

        [Required(ErrorMessage = "Для публикации статьи надо залогиниться")]
        public string PostAuthorEmail { get; set; }
        public List<Tag>? PostTags { get; set; }

    }
}
