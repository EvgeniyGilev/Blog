namespace BlogWebApp.BLL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Role(int id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }
    }
}
