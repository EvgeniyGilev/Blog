using BlogAPI.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Contracts.Models.Comments
{

    public class CreateCommentModel
    {
        [Required(ErrorMessage = "Id статьи, к которой добавляется комментарий")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Текст комментария должен быть не пустым")]
        public string CommentText { get; set; }

    }
}
