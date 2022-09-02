using BlogAPI.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Posts
{

    public class EditPostModel
    {
        [Required(ErrorMessage = "Название Статьи должно быть заполнено")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Содержание Статьи должно быть заполнено")]
        public string PostText { get; set; }
        public int PostId { get; set; }
        public List<Tag>? PostTagsCurrent { get; set; }
        public List<Tag>? PostTagsAll { get; set; }

    }
}
