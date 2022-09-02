using BlogAPI.DATA.Models;

namespace BlogAPI.Contracts.Models.Posts
{

    public class ShowPostsModel
    {
        public Post[] ShowPosts { get; set; }
        public string MiniPost(Post miniPost)
        {
            if (miniPost.postText.Length > 300)
            { return miniPost.postText.Substring(0, 300) + " ...."; }

            else
            {
                return miniPost.postText;
            }

        }
    }
}


