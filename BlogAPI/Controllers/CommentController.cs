// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Comments;
using BlogAPI.DATA.Models;
using BlogAPI.Handlers;
using BlogAPI.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    /// <summary>
    /// Действия с комментариями.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("api/comment/")]
    public class CommentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="userManager">UserManager AspNetCore.Identity.</param>
        /// <param name="logger">NLOG logger.</param>
        /// <param name="commentService"> комментарии. </param>
        /// <param name="postService"> статьи. </param>
        public CommentController(UserManager<User> userManager, ILogger<CommentController> logger,ICommentService commentService, IPostService postService)
        {
            _userManager = userManager;
            _logger = logger;
            _commentService = commentService;
            _postService = postService;
        }

        /// <summary>
        /// Создаем комментарий у статьи, требуется вход пользователя.
        /// </summary>
        /// <param name="newComment"> Форма комментария, указывается ID статьи и тело комментария.</param>
        /// <response code="200">Комментарий успешно добавлен.</response>
        /// <response code="400">Комментарий добавить не удалось.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращается сообщение со статусом создания комментария в формате JSON.</returns>
        // GET: CommentController/Create
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm] CreateCommentModel newComment)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                var currentUser = User;
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.FindByIdAsync(currentUserId);
                if (user != null)
                {
                    var post = await _postService.GetPostById(newComment.PostId);

                    var comment = new Comment
                    {
                        CommentTexte = newComment.CommentText,
                        Post = post,
                        User = user,
                    };

                    var isCommentCreate = await _commentService.CreateComment(comment);
                    if (isCommentCreate)
                    {
                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            InfoMessage = "Комментарий успешно добавлен к статье: \"" + post.PostName + "\" пользователем: " + user.Email,
                        };

                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogError("Ошибка при добавлении комментария");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Ошибка при добавлении комментария", ErrorCode = 40001 }).Value);
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
        /// Удалить комментарий по ID (int).
        /// </summary>
        /// <param name="id"> ID (int) комментария.</param>
        /// <response code="200">Комментарий успешно удален.</response>
        /// <response code="400">Комментарий удалить не удалось.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращается сообщение со статусом удаления, JSON.</returns>
        // GET: CommentController/Delete/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (User.IsInRole("Администратор"))
            {
                var comment = await _commentService.GetCommentById(id);
                var isCommentDelete = await _commentService.DeleteComment(comment);
                if (isCommentDelete)
                {
                    SuccessResponse resp = new()
                    {
                        Code = 0,
                        InfoMessage = "Комментарий успешно удален",
                    };

                    return Json(resp);
                }
                else
                {
                    _logger.LogError("Ошибка при удалении комментария");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Ошибка при удалении комментария", ErrorCode = 40001 }).Value);
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
