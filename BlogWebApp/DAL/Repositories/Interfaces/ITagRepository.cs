using BlogWebApp.BLL.Models.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task CreateTag(Tag tag);
        Task EditTag(Tag tag);
        Task DelTag(Tag tag);
        Task<Tag[]> GetTags();
        Task<Tag?> GetTagById(int id);
    }
}
