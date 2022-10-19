// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.Contracts.Models.Users
{
    /// <summary>
    /// The get all users model.
    /// </summary>
    public class GetAllUsersModel
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public List<ShowUserModel> Users { get; set; }
    }
}
