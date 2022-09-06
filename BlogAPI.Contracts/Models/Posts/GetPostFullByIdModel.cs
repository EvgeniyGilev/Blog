using BlogAPI.Contracts.Models.Tags;
using BlogAPI.Contracts.Models.Comments;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Posts
{

    public class GetPostFullByIdModel
    {
        public int id { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime CreateDate { get; set; }

        //ссылка на теги
        public virtual List<TagView> Tags { get; set; }

        //ссылка на комментарии
        public virtual List<CommentView> Comments { get; set; }
    }

}


