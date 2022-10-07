namespace BlogAPI.Services
{
    using BlogAPI.Contracts.Models;
    using BlogAPI.DATA.Models;
    using BlogAPI.DATA.Repositories.Interfaces;
    using BlogAPI.Interfaces.Services;

    /// <summary>
    /// The tag service.
    /// </summary>
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class.
        /// </summary>
        /// <param name="tagRepository">tag repository.</param>
        /// <param name="unitOfWork">UoW.</param>
        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
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
                return tag;
            }
            else
            {
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

                return true;
            }
            else
            {
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
                tag.tagText = newTag.tagText;

                await _tagRepository.EditTag(tag, id);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            else
            {
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

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
