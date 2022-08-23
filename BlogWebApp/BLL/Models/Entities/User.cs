namespace BlogWebApp.BLL.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPassword { get; set; }
        
        //email
        public string UserLogin { get; set; }
        public string UserCreateDate { get; set; }

        //ссылка на роли
        public virtual List<Role> Roles { get; set; } = new();

    }
}
