using BlogAPI.DATA.Context;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DATA.Repositories
{
    /// <summary>
    /// The tag repository.
    /// </summary>
    public class TagRepository : ITagRepository
    {
        // ссылка на контекст
        private readonly AppDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TagRepository(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>A Task.</returns>
        public async Task CreateTag(Tag tag)
        {
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
            {
                await _context.Tag.AddAsync(tag);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Del the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>A Task.</returns>
        public async Task DelTag(Tag tag)
        {

            // Удаление тега
            var dbtag = _context.Tag.Where(u => u.tagText == tag.tagText).First();
            if (dbtag != null)
            {
                _context.Tag.Remove(dbtag);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Edits the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task EditTag(Tag tag, int id)
        {
            // изменение тега
            var dbtag = _context.Tag.Where(u => u.id == id).First();
            if (dbtag != null)
            {
                dbtag.tagText = tag.tagText;
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the tag by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task<Tag?> GetTagById(int id)
        {
            var tagById = await _context.Tag.FindAsync(id);

            return tagById;
        }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<List<Tag>> GetTags()
        {
            // Получим все статьи
            return await _context.Tag.Include(t => t.Posts).ToListAsync();
        }

        /// <summary>
        /// Gets the tag by name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>A Task.</returns>
        public async Task<Tag?> GetTagByName(string Name)
        {
            var tagByName = await _context.Tag.FirstOrDefaultAsync(t => t.tagText == Name);

            return tagByName;
        }
    }
}
