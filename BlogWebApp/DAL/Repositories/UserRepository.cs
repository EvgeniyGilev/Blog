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

            //по умолчанию права пользователя
            user.Roles = _context.Role.Where(r => r.Id == 1).ToList();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                await _context.User.AddAsync(user);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task DelUser(User user)
        {
            // Удаление пользователя
            var dbuser = _context.User.Where(u => u.Id == user.Id).First();
            if (dbuser !=null)
                 _context.User.Remove(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task EditUser(User user)
        {
            // изменение данных пользователя
            var dbuser = _context.User.Where(u => u.UserLogin == user.UserLogin).First();
            if (dbuser != null)
            {
                dbuser.UserLogin = user.UserLogin;
                dbuser.UserPassword = user.UserPassword;
                dbuser.UserLastName = user.UserLastName;
                dbuser.UserFirstName = user.UserFirstName;
            }

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

            return await _context.User.Include(u => u.Roles).ToArrayAsync();

        }

        // поиск пользователя по Login
        public User? GetUserByLogin(string login)
        {
            var user = _context.User.Include(u => u.Roles).FirstOrDefault(u => u.UserLogin == login);

            return user;
        }

    }
}
