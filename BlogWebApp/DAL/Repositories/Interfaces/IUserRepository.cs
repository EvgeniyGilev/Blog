using BlogWebApp.BLL.Models.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task EditUser(User user);
        Task DelUser(User user);
        Task<User[]> GetUsers();
        Task<User?> GetUserById(int id);
        User? GetUserByLogin(string login);

    }
}
