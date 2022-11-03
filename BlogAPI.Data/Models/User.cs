// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using Microsoft.AspNetCore.Identity;

namespace BlogAPI.DATA.Models
{
    /// <summary>
    /// The user.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public string? UserFirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string? UserLastName { get; set; }

        /// <summary>
        /// Gets or sets the user create date.
        /// </summary>
        public string? UserCreateDate { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// ссылка на роли.
        /// </summary>
        public virtual List<Role> Roles { get; set; } = new ();
    }
}
