﻿using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Entities;
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
        public async Task CreateTag(TagEntity tag)
        {
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                await _context.Tag.AddAsync(tag);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
        
        //Удаляем тег
        public async Task DelTag(TagEntity tag)
        {
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                _context.Tag.Remove(tag);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        //редактируем тег
        public async Task EditTag(TagEntity tag)
        {
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                _context.Tag.Update(tag);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
        //получаем тег по идентификатору
        public async Task<TagEntity?> GetTagById(int id)
        {
            var tagById = await _context.Tag.FindAsync(id);

            return tagById;
        }

        public async Task<TagEntity[]> GetTags()
        {
            // Получим все статьи
            return await _context.Tag.ToArrayAsync();
        }
    }
}
