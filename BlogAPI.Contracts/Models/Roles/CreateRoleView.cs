using BlogAPI.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Roles
{

    public class CreateRoleView
    {
        [Required(ErrorMessage = "Требуется заполнить \"Название Роли\" ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Требуется заполнить \"Описание Роли\" ")]
        public string Description { get; set; }
    }
}
