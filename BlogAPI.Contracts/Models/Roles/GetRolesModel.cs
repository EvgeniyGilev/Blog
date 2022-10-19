// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.Contracts.Models.Roles
{
    /// <summary>
    /// The get roles model.
    /// </summary>
    public class GetRolesModel
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<ShowRoleView> Roles { get; set; }
    }
}
