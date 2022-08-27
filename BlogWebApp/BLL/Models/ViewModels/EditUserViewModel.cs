using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogWebApp.BLL.Models.ViewModels
{

    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPassword { get; set; }

    }
}
