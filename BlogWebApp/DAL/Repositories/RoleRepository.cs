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
            // Удаление пользователя
            var dbrole = _context.Role.Where(u => u.RoleName == role.RoleName).First();
            if (dbrole != null)
                _context.Role.Remove(dbrole);
            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        //редактируем тег
        public async Task EditRole(Role role, int id)
        {
            // изменение тега
            var dbrole = _context.Role.Where(u => u.Id == id).First();
            if (dbrole != null)
            {
                dbrole.RoleName = role.RoleName;
                dbrole.RoleDescription = role.RoleDescription;
            }
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
