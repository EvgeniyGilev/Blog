using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Interfaces.Services;

namespace BlogAPI.Services
{
    /// <summary>
    /// The comment service.
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CommentService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="_commentRepository"></param>
        /// <param name="unitOfWork"></param>
        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, ILogger<CommentService> logger)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Create the comment.
        /// </summary>
        /// <param name="newComment">The new comment.</param>
        /// <returns>true or false.</returns>
        async Task<bool> ICommentService.CreateComment(Comment newComment)
        {
            try
            {
                await _commentRepository.CreateComment(newComment);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при создании комментария: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="id">The id comment.</param>
        /// <returns>true or false.</returns>
        async Task<bool> ICommentService.DeleteComment(Comment comment)
        {
            try
            {
                await _commentRepository.DelComment(comment);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при удалении комментария: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the comment by id.
        /// </summary>
        /// <param name="id">The id comment.</param>
        /// <returns>Comment.</returns>
        async Task<Comment?> ICommentService.GetCommentById(int id)
        {
            return await _commentRepository.GetCommentById(id);
        }
    }
}
