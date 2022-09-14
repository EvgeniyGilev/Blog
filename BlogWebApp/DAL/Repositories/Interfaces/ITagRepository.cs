using BlogWebApp.BLL.Models.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    /// <summary>
    /// The tag repository.
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>
        /// Creates the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>A Task.</returns>
        Task CreateTag(Tag tag);

        /// <summary>
        /// Edits the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task EditTag(Tag tag, int id);

        /// <summary>
        /// Del the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>A Task.</returns>
        Task DelTag(Tag tag);

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<List<Tag>> GetTags();
        /// <summary>
        /// Gets the tag by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task<Tag?> GetTagById(int id);

        /// <summary>
        /// Gets the tag by name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>A Task.</returns>
        Task<Tag?> GetTagByName(string Name);
    }
}
