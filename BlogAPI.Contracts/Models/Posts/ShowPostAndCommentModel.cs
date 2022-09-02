using BlogAPI.DATA.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Posts
{

    public class ShowPostAndCommentModel
    {
        public Post? ShowPost { get; set; }
        [Required(ErrorMessage = "Комментарий не может быть пустым")]
        public string Comment { get; set; }
        public int PostId { get; set; }
    }
}


