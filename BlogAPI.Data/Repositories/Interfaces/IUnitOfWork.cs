namespace BlogAPI.DATA.Repositories.Interfaces
{
    /// <summary>
    /// The Unit of Work Pattern interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Asynchronously complete data management operations.
        /// </summary>
        /// <returns>A Task.</returns>
        Task CompleteAsync();
    }
}
