using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.DATA.Context;

namespace BlogAPI.DATA.Repositories
{
    /// <summary>
    /// The Unit of Work Pattern class.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously complete data management operations.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
