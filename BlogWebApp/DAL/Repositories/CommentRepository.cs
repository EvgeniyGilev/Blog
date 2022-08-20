using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        // ссылка на контекст
        private readonly AppDBContext _context;

        // Метод-конструктор для инициализации
        public CommentRepository(AppDBContext context)
        {
            _context = context;
        }

        // Добавление комментария
        public async Task CreateComment(CommentEntity comment)
        {
            comment.commentCreatedDate = DateTime.Now.ToString();

            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                await _context.Comment.AddAsync(comment);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
        // Удаление комментария
        public async Task DelComment(CommentEntity comment)
        {
            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                _context.Comment.Remove(comment);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        // редактирование комментария
        public async Task EditComment(CommentEntity comment)
        {
            
            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                _context.Comment.Update(comment);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task<CommentEntity?> GetCommentById(int id)
        {
            var commentById = await _context.Comment.FindAsync(id);

            return commentById;
        }

        public async Task<CommentEntity[]> GetComments()
        {
            // Получим все статьи
            return await _context.Comment.ToArrayAsync();
        }
    }
}
