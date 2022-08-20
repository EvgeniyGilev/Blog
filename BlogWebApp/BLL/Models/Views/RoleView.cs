namespace BlogWebApp.BLL.Models.Views
{
    public class RoleView
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public RoleView(int id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }
    }
}
