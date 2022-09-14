using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.TagViews
{
    /// <summary>
    /// The create tag view model.
    /// </summary>
    public class CreateTagViewModel
    {
        /// <summary>
        /// Gets or sets the tag text.
        /// </summary>
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Длина тега должна быть больше 3х, но меньше 10-ти символов")]
        [Required(ErrorMessage = "Название тега должно быть заполнено")]
        public string tagText { get; set; }
    }
}
