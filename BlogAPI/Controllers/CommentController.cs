using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Comments;
using BlogAPI.Contracts.Models.Posts;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    /// <summary>
    /// Действия с комментариями
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repo;
        private readonly IPostRepository _repoposts;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentRepository repo, IPostRepository repoposts, UserManager<User> userManager, ILogger<CommentController> logger)
        {
            _repo = repo;
            _repoposts = repoposts;
            _userManager = userManager;
            _logger = logger;
        }


        /// <summary>
        /// Создаем комментарий у статьи, требуется аутентификация пользователя.
        /// </summary>
        /// <param name="newComment"> Форма комментария, указывается ID статьи и тело комментария</param>
        /// <response code="200">Комментарий успешно добавлен</response>
        /// <response code="400">Комментарий добавить не удалось</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: CommentController/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreateCommentModel newComment)
        {
            if (User.Identity.IsAuthenticated)
            {
                ClaimsPrincipal currentUser = User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _userManager.FindByIdAsync(currentUserID);
                if (user != null)
                {
                    var post = await _repoposts.GetPostById(newComment.PostId);
                    if (post != null)
                    {

                        Comment comment = new Comment();
                        
                        comment.commentTexte = newComment.CommentText;
                        comment.Post = post;
                        comment.User = user;

                        await _repo.CreateComment(comment);

                        SuccessResponse resp = new()
                        {
                            code = 0,
                            infoMessage = "Комментарий успешно добавлен к статье: \"" + post.postName + "\" пользователем: " + user.Email
                        };

                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogInformation("Статья не найдена");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
                    }
                }
                else
                {
                    _logger.LogWarning("Пользователь не найден при добавлении комментария");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Пользователь не найден при добавлении комментария", ErrorCode = 40011 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Чтобы оставить комментарий требуется залогиниться");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Чтобы оставить комментарий требуется залогиниться", ErrorCode = 40011 }).Value);
            }

        }


        /// <summary>
        /// Удалить комментарий по ID (int)
        /// </summary>
        /// <param name="id"> ID (int) комментария</param>
        /// <response code="200">Комментарий успешно удален</response>
        /// <response code="400">Комментарий удалить не удалось</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: CommentController/Delete/5
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("Администратор"))
            {
                var comment = await _repo.GetCommentById(id);
                if (comment != null)
                {
                    await _repo.DelComment(comment);
                    SuccessResponse resp = new()
                    {
                        code = 0,
                        infoMessage = "Комментарий успешно удален"
                    };

                    return Json(resp);
                }
                else
                {
                    _logger.LogInformation("Комментарий не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Комментарий не найден", ErrorCode = 40001 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }
    }
}
