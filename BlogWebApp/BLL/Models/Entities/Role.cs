namespace BlogWebApp.BLL.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        // ссылка на пользователей
        public List<User> Users { get; set; } = new();
    }
}
