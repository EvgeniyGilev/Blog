using Microsoft.AspNetCore.Identity;

namespace BlogAPI.DATA.Models
{
    /// <summary>
    /// The role.
    /// </summary>
    public class Role : IdentityRole
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
