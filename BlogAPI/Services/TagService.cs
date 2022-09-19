using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Interfaces.Services;

namespace BlogAPI.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tag>> ListAsync()
        {
            return await _tagRepository.GetTags();
        }

    }
}
