// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Tags
{
    /// <summary>
    /// The edit tag model.
    /// </summary>
    public class EditeTagModel
    {
        /// <summary>
        /// Gets or sets the tag text.
        /// </summary>
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Длина тега должна быть больше 3х, но меньше 10-ти символов")]
        [Required(ErrorMessage = "Название тега должно быть заполнено")]
        public string tagText { get; set; }
    }
}
