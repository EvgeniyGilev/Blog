﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Users
{
    /// <summary>
    /// The create user model.
    /// </summary>
    public class CreateUserModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required(ErrorMessage = "Заполните поле \"Email\" (логин пользователя)")]
        [EmailAddress(ErrorMessage = "Email введен некорректно")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        [Required(ErrorMessage = "Требуется указать \"Имя\" пользователя")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я'][a-zA-Zа-яА-Я-' ]+[a-zA-Zа-яА-Я']?$", ErrorMessage = "Некорректное имя")]
        public string UserFirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        [Required(ErrorMessage = "Требуется указать \"Фамилию\" пользователя")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я'][a-zA-Zа-яА-Я-' ]+[a-zA-Zа-яА-Я']?$", ErrorMessage = "Некорректная фамилия")]
        public string UserLastName { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        [JsonIgnore]
        [Required(ErrorMessage = "Поле \"Пароль\" обязательное для заполнения")]
        [StringLength(8, ErrorMessage = "{0} Должен быть минимум {2} символа", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string UserPassword { get; set; }
    }
}
