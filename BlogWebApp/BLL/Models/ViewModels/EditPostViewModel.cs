using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogWebApp.BLL.Models.ViewModels
{

    public class EditPostViewModel
    {
        public string PostName { get; set; }
        public string PostText { get; set; }
        public int PostId { get; set; }
        public List<Tag> ?PostTagsCurrent{ get; set; }
        public List<Tag> ?PostTagsAll { get; set; }

    }
}
