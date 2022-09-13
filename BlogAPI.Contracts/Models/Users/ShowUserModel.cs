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
