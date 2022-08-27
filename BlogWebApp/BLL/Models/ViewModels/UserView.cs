namespace BlogWebApp.BLL.Models.Views
{
    public class UserView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string UserPassword { get; set; }
        public string UserLogin { get; set; }
        public DateTime UserCreatedDate { get; set; }

        public UserView(int id, string userName, int roleId, string userPassword, string userLogin, DateTime userCreatedDate)
        {
            Id = id;
            UserName = userName;
            RoleId = roleId;
            UserPassword = userPassword;
            UserLogin = userLogin;
            UserCreatedDate = userCreatedDate;
        }
    }
}
