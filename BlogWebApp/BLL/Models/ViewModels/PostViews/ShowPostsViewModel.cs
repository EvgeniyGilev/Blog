using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogWebApp.BLL.Models.ViewModels.PostViews
{

    public class ShowPostsViewModel
    {
        public Post[] ShowPosts { get; set; }
        public string MiniPost(Post miniPost)
        {
            if (miniPost.postText.Length > 300)
            { return miniPost.postText.Substring(0, 300) + " ...."; }

            else
            {
                return miniPost.postText;
            }

        }
    }
}


