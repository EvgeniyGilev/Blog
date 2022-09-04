using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Contracts.Models.Posts
{

    public class GetPostByIdModel
    {
        public int id { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime CreateDate { get; set; }
    }

}


