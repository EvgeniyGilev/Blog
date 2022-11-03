// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using Microsoft.AspNetCore.Identity;

namespace BlogAPI.DATA.Models
{
    /// <summary>
    /// The role.
    /// </summary>
    public sealed class Role : IdentityRole
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }

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

        public Role()
        {
        }
    }
}
