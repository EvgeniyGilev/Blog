// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.RoleViews
{
    /// <summary>
    /// The create role view model.
    /// </summary>
    public class CreateRoleViewModel
    {
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
