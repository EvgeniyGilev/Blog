using BlogWebApp.BLL.Models.Entities;

namespace BlogWebApp.DAL.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task CreateRole(Role role);
        Task EditRole(Role role);
        Task DelRole(Role role);
        Task<Role[]> GetRoles();
        Task<Role?> GetRoleById(int id);
    }
}
