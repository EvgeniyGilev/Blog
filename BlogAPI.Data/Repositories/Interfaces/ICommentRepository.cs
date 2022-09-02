using BlogAPI.DATA.Models;

namespace BlogAPI.DATA.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateComment(Comment comment);
        Task EditComment(Comment comment);
        Task DelComment(Comment comment);
        Task<Comment[]> GetComments();
        Task<Comment?> GetCommentById(int id);

    }
}
