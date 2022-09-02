using BlogAPI.DATA.Models;

namespace BlogAPI.Contracts.Models.Roles
{

    public class ChangeRoleModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<Role> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleModel()
        {
            AllRoles = new List<Role>();
            UserRoles = new List<string>();
        }
    }
}
