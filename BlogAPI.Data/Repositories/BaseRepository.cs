// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.DATA.Context;

namespace BlogAPI.DATA.Repositories
{
    /// <summary>
    /// The base repository.
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="context">The context AppDBContext.</param>
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}