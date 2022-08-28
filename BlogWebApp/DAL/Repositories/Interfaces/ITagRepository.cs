using BlogWebApp.BLL.Models.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task CreateTag(Tag tag);
        Task EditTag(Tag tag, int id);
        Task DelTag(Tag tag);
        Task<List<Tag>> GetTags();
        Task<Tag?> GetTagById(int id);
    }
}
