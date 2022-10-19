// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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
