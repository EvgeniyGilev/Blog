// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.TagViews
{
    /// <summary>
    /// The edit tag view model.
    /// </summary>
    public class EditeTagViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Required(ErrorMessage = "Id тега должно быть заполнено")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the tag text.
        /// </summary>
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Длина тега должна быть больше 3х, но меньше 10-ти символов")]
        [Required(ErrorMessage = "Название тега должно быть заполнено")]

        public string? TagText { get; set; }
    }
}
