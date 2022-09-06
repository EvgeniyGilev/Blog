using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Users
{

    public class CreateUserModel
    {
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

        [JsonIgnore]
        [Required(ErrorMessage = "Поле \"Пароль\" обязательное для заполнения")]
        [StringLength(8, ErrorMessage = "{0} Должен быть минимум {2} символа", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string UserPassword { get; set; }

    }
}
