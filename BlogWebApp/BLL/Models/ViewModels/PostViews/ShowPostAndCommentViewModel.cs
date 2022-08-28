using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{

    public class ShowPostAndCommentViewModel
    {
        public Post? ShowPost { get; set; }
        public string? Comment { get; set; }
        public int PostId { get; set; }
    }
}


