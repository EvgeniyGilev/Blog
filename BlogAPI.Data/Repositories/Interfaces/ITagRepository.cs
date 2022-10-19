// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;

namespace BlogAPI.DATA.Repositories.Interfaces
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
