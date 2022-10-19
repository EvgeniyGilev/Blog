// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.Contracts.Models.Users
{
    /// <summary>
    /// The show user model.
    /// </summary>
    public class ShowUserModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public IList<string> Roles { get; set; }
    }
}
