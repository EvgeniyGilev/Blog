using BlogAPI.DATA.Models;

namespace BlogAPI.Contracts.Models.Posts
{

    public class GetPostsModel
    {
        public int PostsCount { get; set; }
        public PostView[] Posts { get; set; }
        public class PostView
        {
            public int id { get; set; }
            public string PostTitle { get; set; }
            public string PostText { get; set; }
            public string AuthorEmail { get; set; }
            public DateTime CreateDate { get; set; }
        }
        public string MiniPost(PostView miniPost)
        {
            if (miniPost.PostText.Length > 300)
            { return miniPost.PostText.Substring(0, 300) + " ...."; }

            else
            {
                return miniPost.PostText;
            }

        }
    }
}


