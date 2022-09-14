using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Roles
{
    /// <summary>
    /// The show role view.
    /// </summary>
    public class ShowRoleView
    {
        /// <summary>
        /// Gets or sets the id.
        /// GUID роли.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessage = "Требуется заполнить \"Название Роли\" ")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required(ErrorMessage = "Требуется заполнить \"Описание Роли\" ")]
        public string Description { get; set; }
    }
}
