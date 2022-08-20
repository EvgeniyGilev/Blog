namespace BlogWebApp.BLL.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserLogin { get; set; }
        public string UserCreateDate { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
