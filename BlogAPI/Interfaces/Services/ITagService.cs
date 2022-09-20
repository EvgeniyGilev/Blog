using BlogAPI.DATA.Models;

namespace BlogAPI.Interfaces.Services
{
    /// <summary>
    /// The tag service.
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Вывод списка тегов.
        /// </summary>
        /// <returns>List of Tags.</returns>
        Task<IEnumerable<Tag>> ListAsync();

        /// <summary>
        /// Вывод тега по его id.
        /// </summary>
        /// <param name="id">id тега.</param>
        /// <returns>Tag.</returns>
        Task<Tag> GetTagById(int id);

        /// <summary>
        /// Создание тега.
        /// </summary>
        /// <param name="name">Имя нового тега.</param>
        /// <returns>Boolean - true - success or false - failure.</returns>
        Task<bool> CreateTag(string name);

        /// <summary>
        /// редактирование существующего тега.
        /// </summary>
        /// <param name="id">id тега.</param>
        /// <param name="newTag"> новый тег.</param>
        /// <returns>Boolean - true - success or false - failure.</returns>
        Task<bool> EditTag(int id, Tag newTag);

        /// <summary>
        /// Удаление тега по его id.
        /// </summary>
        /// <param name="id">id тега.</param>
        /// <returns>Boolean - true - success or false - failure..</returns>
        Task<bool> DeleteTag(int id);
    }
}
