using BlogWebApp.DAL.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task CreateTag(TagEntity tag);
        Task EditTag(TagEntity tag);
        Task DelTag(TagEntity tag);
        Task<TagEntity[]> GetTags();
        Task<TagEntity?> GetTagById(int id);
    }
}
