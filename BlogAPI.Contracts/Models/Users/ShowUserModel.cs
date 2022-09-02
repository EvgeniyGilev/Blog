namespace BlogAPI.Contracts.Models.Users
{

    public class ShowUserModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public ShowUserModel()
        {
            UserRoles = new List<string>();
        }
    }
}
