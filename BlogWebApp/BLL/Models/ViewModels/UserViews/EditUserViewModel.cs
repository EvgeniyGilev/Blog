﻿using BlogWebApp.BLL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.BLL.Models.ViewModels.UserViews
{

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Email\" (логин пользователя)")]
        [EmailAddress(ErrorMessage = "Email введен некорректно")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется указать \"Имя\" пользователя")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я'][a-zA-Zа-яА-Я-' ]+[a-zA-Zа-яА-Я']?$", ErrorMessage = "Некорректное имя")]
        public string UserFirstName { get; set; }

        [Required(ErrorMessage = "Требуется указать \"Фамилию\" пользователя")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я'][a-zA-Zа-яА-Я-' ]+[a-zA-Zа-яА-Я']?$", ErrorMessage = "Некорректная фамилия")]
        public string UserLastName { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" обязательное для заполнения")]
        [StringLength(8, ErrorMessage = "{0} Должен быть минимум {2} символа", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string UserPassword { get; set; }

    }
}