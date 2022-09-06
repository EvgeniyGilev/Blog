namespace BlogAPI.Contracts.Models.Users
{
    public class ShowUserModel
    {
        public string id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IList<string> Roles { get; set; }

    }
}
