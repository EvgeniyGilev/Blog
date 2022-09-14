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
