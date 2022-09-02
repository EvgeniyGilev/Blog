using BlogAPI.DATA.Models;

namespace BlogAPI.DATA.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task CreateTag(Tag tag);
        Task EditTag(Tag tag, int id);
        Task DelTag(Tag tag);
        Task<List<Tag>> GetTags();
        Task<Tag?> GetTagById(int id);
        Task<Tag?> GetTagByName(string Name);

    }
}
