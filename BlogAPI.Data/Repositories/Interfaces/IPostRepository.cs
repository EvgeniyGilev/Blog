using BlogAPI.DATA.Models;

namespace BlogAPI.DATA.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task CreatePost(Post post);
        Task EditPost(Post post,int id);
        Task DelPost(Post post);
        Task<Post[]> GetPosts();
        Task<Post[]> GetPostsByUserId(string id);
        Task<Post?> GetPostById(int id);

    }
}
