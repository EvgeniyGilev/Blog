namespace BlogAPI.Contracts.Models.Users
{
    public class GetAllUsersModel
    {
        public int UsersCount { get; set; }

        public List<ShowUserModel> Users { get; set; }

    }
}
