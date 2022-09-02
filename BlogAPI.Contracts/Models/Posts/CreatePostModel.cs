using BlogAPI.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Posts
{

    public class CreatePostModel
    {
        [Required(ErrorMessage = "Название Статьи должно быть заполнено")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Содержание Статьи должно быть заполнено")]
        public string PostText { get; set; }

        [Required(ErrorMessage = "Для публикации статьи надо залогиниться")]
        public string PostAuthorEmail { get; set; }
        public List<Tag>? PostTags { get; set; }

    }
}
