using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.RoleViews
{
    /// <summary>
    /// The edit role view model.
    /// </summary>
    public class EditeRoleViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

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
