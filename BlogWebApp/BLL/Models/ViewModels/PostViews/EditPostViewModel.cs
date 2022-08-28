using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{

    public class EditPostViewModel
    {
        [Required(ErrorMessage = "Название Статьи должно быть заполнено")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Содержание Статьи должно быть заполнено")]
        public string PostText { get; set; }
        public int PostId { get; set; }
        public List<Tag>? PostTagsCurrent { get; set; }
        public List<Tag>? PostTagsAll { get; set; }

    }
}
