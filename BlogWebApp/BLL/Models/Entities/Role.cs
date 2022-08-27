using Microsoft.AspNetCore.Identity;

namespace BlogWebApp.BLL.Models.Entities
{
    public class Role : IdentityRole
    {
        public string Description { get; set; } = null;

        public Role()
        { }
        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
