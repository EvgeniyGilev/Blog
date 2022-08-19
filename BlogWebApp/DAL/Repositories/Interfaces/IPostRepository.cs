using BlogWebApp.DAL.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task CreatePost(PostEntity post);
        Task EditPost(PostEntity post);
        Task DelPost(PostEntity post);
        Task<PostEntity[]> GetPosts();
        Task<PostEntity[]> GetPostsByUserId(int id);
        Task<PostEntity?> GetPostById(int id);

    }
}
