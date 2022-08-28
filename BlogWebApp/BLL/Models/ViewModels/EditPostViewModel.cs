using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogWebApp.BLL.Models.ViewModels
{

    public class EditPostViewModel
    {
        public string PostName { get; set; }
        public string PostText { get; set; }
        public List<Tag> PostTags{ get; set; }

    }
}
