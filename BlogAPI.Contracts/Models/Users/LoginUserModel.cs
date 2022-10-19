// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Users
{
    /// <summary>
    /// The login user model.
    /// </summary>
    public class LoginUserModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Email\" (логин пользователя)")]
        [EmailAddress(ErrorMessage = "Email введен некорректно")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonIgnore]
        [Required(ErrorMessage = "Поле \"Пароль\" обязательное для заполнения")]
        [StringLength(8, ErrorMessage = "{0} Должен быть минимум {2} символа", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
