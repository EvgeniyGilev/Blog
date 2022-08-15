using BlogWebApp.DAL.Entities;

namespace BlogWebApp.DAL.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(UserEntity user);
        Task EditUser(UserEntity user);
        Task DelUser(UserEntity user);
        Task<string[]> GetUsers();
        Task<UserEntity?> GetUserById(int id);

    }
}
