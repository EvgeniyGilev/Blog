using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Data.Repositories.Interfaces
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
