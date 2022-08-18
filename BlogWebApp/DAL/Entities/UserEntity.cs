namespace BlogWebApp.DAL.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string UserPassword { get; set; }
        public string UserLogin { get; set; }
        public string UserCreateDate { get; set; }

    }
}
