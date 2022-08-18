using BlogWebApp.DAL.Entities;
using BlogWebApp.DAL.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task AddUser(UserEntity user)
        {
            user.UserCreatedDate = DateTime.Now;
            
            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.User.AddAsync(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task DelUser(UserEntity user)
        {
            // Удаление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                 _context.User.Remove(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task EditUser(UserEntity user)
        {
            // изменение данных пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                 _context.User.Update(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        // поиск пользователя по Id
        public async Task<UserEntity?> GetUserById(int id)
        {
            var searchUser = await _context.User.FindAsync(id);

            return  searchUser;
        }

        public async Task<string[]> GetUsers()
        {
            // Получим всех пользователей
            return await _context.User.Select(u => u.UserName).ToArrayAsync();
        }
    }
}
