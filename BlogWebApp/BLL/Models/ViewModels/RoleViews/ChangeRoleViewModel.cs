// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;

namespace BlogWebApp.BLL.Models.ViewModels.RoleViews
{
    /// <summary>
    /// The change role view model.
    /// </summary>
    public class ChangeRoleViewModel
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string? UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the all roles.
        /// </summary>
        public List<Role> AllRoles { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        public IList<string> UserRoles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeRoleViewModel"/> class.
        /// </summary>
        public ChangeRoleViewModel()
        {
            AllRoles = new List<Role>();
            UserRoles = new List<string>();
        }
    }
}
