namespace BlogAPI.Contracts.Models.Users
{
    public class GetAllUsersModel
    {
        public int Count { get; set; }

        public List<ShowUserModel> Users { get; set; }

    }
}
