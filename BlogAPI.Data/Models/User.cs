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
        /// Gets or sets the user password.
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user create date.
        /// </summary>
        public string? UserCreateDate { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// ссылка на роли.
        /// </summary>
        public virtual List<Role> Roles { get; set; } = new ();

        /// <summary>
        /// Gets or sets the posts.
        /// ссылка на статьи.
        /// </summary>
        public virtual List<Post> Posts { get; set; } = new ();

        /// <summary>
        /// Gets or sets the comments.
        /// ссылка на комментарии.
        /// </summary>
        public virtual List<Comment> Comments { get; set; } = new ();
    }
}
