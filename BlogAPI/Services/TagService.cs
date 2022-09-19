using BlogAPI.Contracts.Models;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Interfaces.Services;

namespace BlogAPI.Services
{
    /// <summary>
    /// The tag service.
    /// </summary>
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TagService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class.
        /// </summary>
        /// <param name="tagRepository"></param>
        /// <param name="unitOfWork"></param>
        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, ILogger<TagService> logger)
        {
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        /// <summary>
        /// Get List Tags.
        /// </summary>
        /// <returns>A Task.</returns>
        async Task<IEnumerable<Tag>> ITagService.ListAsync()
        {
            return await _tagRepository.GetTags();
        }

        /// <summary>
        /// Get Tag by Id.
        /// </summary>
        /// <param name="id">id tag, int.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        async Task<Tag?> ITagService.GetTagById(int id)
        {
            var tag = await _tagRepository.GetTagById(id);
            if (tag != null)
            {
                _logger.LogInformation("Получили тег по id: " + id.ToString() + " имя тега: " + tag.tagText);
                return tag;
            }
            else
            {
                _logger.LogInformation("Тег не найден");
                return null;
            }
        }

        /// <summary>
        /// Create new tag.
        /// <param name="name">name tag, string.</param>
        /// </summary>
        /// <returns>true or false.</returns>
        async Task<bool> ITagService.CreateTag(string name)
        {
            // проверяем не существует ли такой тег, если существует то возвращаем false
            var tag = await _tagRepository.GetTagByName(name);
            if (tag == null)
            {
                await _tagRepository.CreateTag(new Tag(name));
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Создан новый тег" + name);

                return true;
            }
            else
            {
                _logger.LogInformation("Тег уже существует");
                return false;
            }
        }
    }
}
