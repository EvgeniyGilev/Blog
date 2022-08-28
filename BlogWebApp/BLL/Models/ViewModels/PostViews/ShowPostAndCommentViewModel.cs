using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{

    public class ShowPostAndCommentViewModel
    {
        public Post? ShowPost { get; set; }
        [Required(ErrorMessage = "Комментарий не может быть пустым")]
        public string Comment { get; set; }
        public int PostId { get; set; }
    }
}


