using BlogAPI.DATA.Models;

namespace BlogAPI.Contracts.Models.Posts
{

    public class GetPostsModel
    {
        public int Count { get; set; }
        public PostView[] Posts { get; set; }
        public class PostView
        {
            public int id { get; set; }
            public string PostTitle { get; set; }
            public string AuthorEmail { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}


