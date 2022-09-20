using BlogAPI.DATA.Context;

namespace BlogAPI.DATA.Repositories
{
    /// <summary>
    /// The base repository.
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly AppDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="context">The context AppDBContext.</param>
        public BaseRepository(AppDBContext context)
        {
            _context = context;
        }
    }
}