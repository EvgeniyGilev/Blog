using BlogWebApp.DAL.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogWebApp.DAL.Repositories.Interfaces;
using BlogWebApp.BLL.Models.Entities;

namespace BlogWebApp.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        // ссылка на контекст
        private readonly AppDBContext _context;

        // Метод-конструктор для инициализации
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddUser(User user)
        {
            user.UserCreateDate = DateTime.Now.ToString();
            user.RoleId = 1; // Пользователь
            
            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.User.AddAsync(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task DelUser(User user)
        {
            // Удаление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                 _context.User.Remove(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task EditUser(User user)
        {
            // изменение данных пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                 _context.User.Update(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        // поиск пользователя по Id
        public async Task<User?> GetUserById(int id)
        {
            var searchUser = await _context.User.FindAsync(id);

            return  searchUser;
        }

        public async Task<User[]> GetUsers()
        {
            // Получим всех пользователей
            return await _context.User.ToArrayAsync();
        }

        // поиск пользователя по Login
        public User? GetUserByLogin(string login)
        {
            var user = _context.User.FirstOrDefault(u => u.UserLogin == login);
            if (user != null)
            {
                var userRole = _context.Role.FirstOrDefault(r =>r.Id == user.RoleId);
                user.Role = userRole;
            }
            return user;
        }

    }
}
