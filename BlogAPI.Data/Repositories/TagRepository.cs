﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Context;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DATA.Repositories
{
    /// <summary>
    /// The tag repository.
    /// </summary>
    public class TagRepository : BaseRepository, ITagRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TagRepository(AppDbContext context)
         : base(context)
        {
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
        }

        /// <summary>
        /// Del the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>A Task.</returns>
        public async Task DelTag(Tag tag)
        {

            // Удаление тега
            var dbtag = _context.Tag.Where(u => u.TagText == tag.TagText).First();
            if (dbtag != null)
            {
                _context.Tag.Remove(dbtag);
            }
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
            var dbtag = _context.Tag.Where(u => u.Id == id).First();
            if (dbtag != null)
            {
                dbtag.TagText = tag.TagText;
            }
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
            var tagByName = await _context.Tag.FirstOrDefaultAsync(t => t.TagText == Name);

            return tagByName;
        }
    }
}
