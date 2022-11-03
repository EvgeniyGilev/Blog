// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.DATA.Context;

namespace BlogAPI.DATA.Repositories
{
    /// <summary>
    /// The Unit of Work Pattern class.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(AppDbContext context)
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
