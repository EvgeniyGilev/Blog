using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogWebApp.BLL.Models.ViewModels.UserViews
{

    public class ShowUserViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public ShowUserViewModel()
        {
            UserRoles = new List<string>();
        }
    }
}
