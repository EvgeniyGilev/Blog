﻿using Microsoft.AspNetCore.Identity;

namespace BlogAPI.DATA.Models
{
    public class User: IdentityUser
    {
       // public string Id { get; set; }
        public string ?UserFirstName { get; set; }
        public string ?UserLastName { get; set; }
        public string UserPassword { get; set; }
        
        //email
        public string Email { get; set; }
        public string ?UserCreateDate { get; set; }

        //ссылка на роли
        public virtual List<Role> Roles { get; set; } = new();

        //ссылка на статьи
        public virtual List<Post> Posts { get; set; } = new();

        //ссылка на комментарии
        public virtual List<Comment> Comments { get; set; } = new();

    }
}
