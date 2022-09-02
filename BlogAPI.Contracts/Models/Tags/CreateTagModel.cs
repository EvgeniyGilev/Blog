﻿using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Tags
{

    public class CreateTagModel
    {
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Длина тега должна быть больше 3х, но меньше 10-ти символов")]
        [Required(ErrorMessage = "Название тега должно быть заполнено")]

        public string tagText { get; set; }

    }
}
