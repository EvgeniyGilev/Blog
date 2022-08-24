using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        // ссылка на контекст
        private readonly AppDBContext _context;

        // Метод-конструктор для инициализации
        public TagRepository(AppDBContext context)
        {
            _context = context;
        }

        //Добавляем тег
        public async Task CreateTag(Tag tag)
        {
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                await _context.Tag.AddAsync(tag);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
        
        //Удаляем тег
        public async Task DelTag(Tag tag)
        {

            // Удаление пользователя
            var dbtag = _context.Tag.Where(u => u.tagText == tag.tagText).First();
            if (dbtag != null)
                _context.Tag.Remove(dbtag);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        //редактируем тег
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
        //получаем тег по идентификатору
        public async Task<Tag?> GetTagById(int id)
        {
            var tagById = await _context.Tag.FindAsync(id);

            return tagById;
        }

        public async Task<Tag[]> GetTags()
        {
            // Получим все статьи
            return await _context.Tag.Include(t => t.Posts).ToArrayAsync();
        }
    }
}
