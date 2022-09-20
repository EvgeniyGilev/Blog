using BlogAPI.DATA.Models;

namespace BlogAPI.Interfaces.Services
{
    /// <summary>
    /// The post service.
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// вывести список статей.
        /// </summary>
        /// <returns>List of Posts.</returns>
        Task<IEnumerable<Post>> ListAsync();

        /// <summary>
        /// Вывод статьи по её id.
        /// </summary>
        /// <param name="id">id статьи.</param>
        /// <returns>Post.</returns>
        Task<Post> GetPostById(int id);

        /// <summary>
        /// Создание статьи.
        /// </summary>
        /// <param name="newpost">Данные для новой статьи.</param>
        /// <returns>id  - номер новой статьи или 0, если статью не удалось добавить.</returns>
        Task<int> CreatePost(Post newpost);

        /// <summary>
        /// редактирование существующей статьи.
        /// </summary>
        /// <param name="id">id статьи.</param>
        /// <param name="newpost"> данные новой статьи.</param>
        /// <returns>Boolean - true - success or false - failure.</returns>
        Task<bool> EditPost(int id, Post newpost);

        /// <summary>
        /// Удаление статьи по её id.
        /// </summary>
        /// <param name="id">id статьи.</param>
        /// <returns>Boolean - true - success or false - failure.</returns>
        Task<bool> DeletePost(int id);
    }
}
