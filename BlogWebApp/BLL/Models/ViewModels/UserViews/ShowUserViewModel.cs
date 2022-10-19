// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogWebApp.BLL.Models.ViewModels.UserViews
{
    /// <summary>
    /// The show user view model.
    /// </summary>
    public class ShowUserViewModel
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        public IList<string> UserRoles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowUserViewModel"/> class.
        /// </summary>
        public ShowUserViewModel()
        {
            UserRoles = new List<string>();
        }
    }
}
