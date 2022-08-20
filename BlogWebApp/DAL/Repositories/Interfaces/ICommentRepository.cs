using BlogWebApp.DAL.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateComment(CommentEntity comment);
        Task EditComment(CommentEntity comment);
        Task DelComment(CommentEntity comment);
        Task<CommentEntity[]> GetComments();
        Task<CommentEntity?> GetCommentById(int id);

    }
}
