// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogWebApp.BLL.Interfaces.Services;

namespace BlogWebApp.BLL.Services
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
        /// <param name="tagRepository">tags rep.</param>
        /// <param name="unitOfWork">UoW.</param>
        /// <param name="logger">logger.</param>
        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, ILogger<TagService> logger)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                _logger.LogInformation("Получили тег по id: " + id.ToString() + " имя тега: " + tag.TagText);
                return tag;
            }
            else
            {
                _logger.LogInformation("Тег не найден");
                return null;
            }
        }

        /// <summary>
        /// Get Tag by name.
        /// </summary>
        /// <param name="name">name tag, string.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        async Task<Tag?> ITagService.GetTagByName(string name)
        {
            var tag = await _tagRepository.GetTagByName(name);
            if (tag != null)
            {
                _logger.LogInformation("Получили тег по имени,имя тега: " + tag.TagText);
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
        /// </summary>
        /// <param name="name">name tag, string.</param>
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

        /// <summary>
        /// Edit tag by Id.
        /// </summary>
        /// <param name="id">id tag, int.</param>
        /// <param name="newTag">new tag, Tag.</param>
        /// <returns>true or false.</returns>
        async Task<bool> ITagService.EditTag(int id, Tag newTag)
        {
            var tag = await _tagRepository.GetTagById(id);
            if (tag != null)
            {
                tag.TagText = newTag.TagText;

                await _tagRepository.EditTag(tag, id);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Изменили тег по id: " + id.ToString() + " новое имя тега: " + newTag.TagText);

                return true;
            }
            else
            {
                _logger.LogInformation("Тег не найден");
                return false;
            }
        }

        /// <summary>
        /// Delete tag by Id.
        /// </summary>
        /// <param name="id">id tag, int.</param>
        /// <returns>true or false.</returns>
        async Task<bool> ITagService.DeleteTag(int id)
        {
            var tag = await _tagRepository.GetTagById(id);
            if (tag != null)
            {
                await _tagRepository.DelTag(tag);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Удалили тег по id: " + id + " имя тега: " + tag.TagText);

                return true;
            }
            else
            {
                _logger.LogInformation("Тег не найден");
                return false;
            }
        }
    }
}
