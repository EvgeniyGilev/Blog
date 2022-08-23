using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Context;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        // ссылка на контекст
        private readonly AppDBContext _context;

        // Метод-конструктор для инициализации
        public RoleRepository(AppDBContext context)
        {
            _context = context;
        }

        //Добавляем тег
        public async Task CreateRole(Role role)
        {
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
                await _context.Role.AddAsync(role);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
        
        //Удаляем тег
        public async Task DelRole(Role role)
        {
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
                _context.Role.Remove(role);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        //редактируем тег
        public async Task EditRole(Role role)
        {
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
                _context.Role.Update(role);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }
        //получаем тег по идентификатору
        public async Task<Role?> GetRoleById(int id)
        {
            var roleById = await _context.Role.FindAsync(id);

            return roleById;
        }

        public async Task<Role[]> GetRoles()
        {
            // Получим все статьи
            return await _context.Role.ToArrayAsync();
        }
    }
}
