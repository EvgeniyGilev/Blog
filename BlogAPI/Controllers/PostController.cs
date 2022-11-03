// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using AutoMapper;
using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Comments;
using BlogAPI.Contracts.Models.Posts;
using BlogAPI.Contracts.Models.Tags;
using BlogAPI.DATA.Models;
using BlogAPI.Handlers;
using BlogAPI.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BlogAPI.Contracts.Models.Posts.GetPostsModel;

namespace BlogAPI.Controllers
{
    /// <summary>
    /// Действия с статьями блога.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("api/post/")]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="userManager">UserManager AspNetCore.Identity.</param>
        /// <param name="logger">NLOG logger.</param>
        /// <param name="mapper">Automapper.</param>
        /// <param name="postService">Бизнес логика получения данных по статьям.</param>
        /// <param name="tagService"></param>
        public PostController(UserManager<User> userManager, ILogger<PostController> logger, IMapper mapper, IPostService postService, ITagService tagService)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _postService = postService;
            _tagService = tagService;
        }

        /// <summary>
        /// Получить статью по ID.
        /// </summary>
        /// <remarks>
        /// Ответ будет в виде JSON (POST)
        /// {
        /// "id": 1,
        /// "postTitle": "Название стати",
        /// "postText": "Текст статьи",
        /// "authorEmail": "example@gmail.com",
        /// "createDate": "2022-09-03T13:40:11"
        /// }.
        /// </remarks>
        /// <param name="id"> номер (id) статьи.</param>
        /// <response code="200">Получаем статьи.</response>
        /// <response code="400">Статьи с таким id не существует.</response>
        /// <response code="500">Произошла ошибка.</response>
        /// <returns>Возвращает статью в формате JSON.</returns>
        // GET: PostController
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            var post = await _postService.GetPostById(id);
            if (post.User != null && post.PostCreateDate != null)
            {
                GetPostByIdModel resp = new ()
                {
                    id = post.Id,
                    AuthorEmail = post.User.Email,
                    PostTitle = post.PostName,
                    PostText = post.PostText,
                    CreateDate = DateTime.Parse(post.PostCreateDate),
                };

                _logger.LogInformation("Получаем статью с ID: " + id);

                return Json(resp);
            }

            _logger.LogInformation("По текущему ID не смогли получить статью или её дату создания " + id + "возвращаемся на страницу всех статей.");
            return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
        }

        /// <summary>
        /// Получить статью по ID со всеми комментариями и тегами.
        /// </summary>
        /// <param name="id"> номер (id) статьи.</param>
        /// <response code="200">Получаем статьи.</response>
        /// <response code="400">Статьи с таким id не существует.</response>
        /// <response code="500">Произошла ошибка.</response>
        /// <returns>Возвращает статью в формате JSON.</returns>
        // GET: PostController
        [HttpGet]
        [Route("fullinfo/{id}")]
        public async Task<IActionResult> GetPostFull([FromRoute] int id)
        {
            var post = await _postService.GetPostById(id);
            if (post.User != null && post.PostCreateDate != null)
            {
                GetPostFullByIdModel resp = new()
                {
                    id = post.Id,
                    AuthorEmail = post.User.Email,
                    PostTitle = post.PostName,
                    PostText = post.PostText,
                    CreateDate = DateTime.Parse(post.PostCreateDate),
                    Tags = _mapper.Map<List<Tag>, List<TagView>>(post.Tags),
                    Comments = _mapper.Map<List<Comment>, List<CommentView>>(post.Comments),
                };

                _logger.LogInformation("Получаем статью с ID: " + id);

                return Json(resp);
            }

            _logger.LogInformation("По текущему ID не смогли получить статью  или её дату создания " + id + "возвращаемся на страницу всех статей.");
            return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
        }

        /// <summary>
        /// Получить все статьи.
        /// </summary>
        /// <response code="200">Получаем статьи.</response>
        /// <response code="400">Возникла ошибка при получении статей.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Список статей в формате JSON.</returns>
        // GET: PostController
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _postService.ListAsync();
                var enumerable = posts.ToList();
                GetPostsModel resp = new ()
                {
                    Count = enumerable.Count,
                    Posts = _mapper.Map<IEnumerable<Post>, List<PostView>>(enumerable),
                };

                _logger.LogInformation("Показываем все статьи, всего статей найдено: " + resp.Count);

                return Json(resp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Возникла ошибка при получении статей " + ex.Message);
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Возникла ошибка при получении статей " + ex.Message, ErrorCode = 40002 }).Value);
            }
        }

        /// <summary>
        /// Добавить статью.
        /// </summary>
        /// <param name="newPost">Данные для статьи.</param>
        /// <response code="200">Добавление статью произошло успешно.</response>
        /// <response code="400">Добавить статью не удалось, так как автор статьи не найден.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение о статусе добавления статьи в формате JSON.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm] CreatePostModel newPost)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                var searchuser = _userManager.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (searchuser != null)
                {
                    Post post = new ()
                    {
                        PostName = newPost.PostName,
                        PostText = newPost.PostText,
                        User = searchuser,
                    };
                    try
                    {
                        var idnewpost = await _postService.CreatePost(post);

                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            Id = idnewpost.ToString(),
                            Name = post.PostName,
                            InfoMessage = "Статья успешно добавлена",
                        };

                        return Json(resp);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Произошла ошибка при создании статьи " + ex.Message);
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Произошла ошибка при создании статьи", ErrorCode = 40003 }).Value);
                    }
                }

                _logger.LogInformation("Автор статьи не найден");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Автор статьи не найден", ErrorCode = 40003 }).Value);
            }

            _logger.LogInformation("Автор статьи не найден");
            return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован", ErrorCode = 40004 }).Value);
        }

        /// <summary>
        /// Редактируем статью по её id.
        /// </summary>
        /// <param name="newPost">новые данные статьи.</param>
        /// <param name="id"> номер (id) статьи.</param>
        /// <response code="200">Изменение статьи произошло успешно.</response>
        /// <response code="400">Изменить статью не удалось, так как статья не найдена.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом редактирования, JSON.</returns>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit([FromForm] EditPostModel newPost, [FromRoute] int id)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                var post = await _postService.GetPostById(id);
                if (post.User != null && ((User.Identity.Name == post.User.UserName) || User.IsInRole("Администратор")))
                {
                    post.PostName = newPost.PostName;
                    post.PostText = newPost.PostText;

                    var isPostEdite = await _postService.EditPost(id, post);
                    if (isPostEdite)
                    {
                        _logger.LogInformation("Статья отредактирована: " + post.PostName);

                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            Id = post.Id.ToString(),
                            Name = post.PostName,
                            InfoMessage = "Статья успешно отредактирована",
                        };

                        return Json(resp);
                    }

                    return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Ошибка при редактировании статьи", ErrorCode = 40004 }).Value);
                }

                return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован или недостаточно прав", ErrorCode = 40004 }).Value);
            }

            _logger.LogInformation("Автор статьи не авторизован или недостаточно прав");
            return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован или недостаточно прав", ErrorCode = 40004 }).Value);
        }

        /// <summary>
        /// Удаляем статью по её id.
        /// </summary>
        /// <param name="id"> номер (id) статьи.</param>
        /// <response code="200">Удаление статьи произошло успешно.</response>
        /// <response code="400">Удалить статью не удалось, так как автор статьи не авторизован или недостаточно прав.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение о статусе удаления статьи, JSON.</returns>
        // Delete: PostController/Delete/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var post = await _postService.GetPostById(id);

                // удалить статью может только автор или администратор
                if ((User.Identity?.Name == post.User?.UserName) || User.IsInRole("Администратор"))
                {
                    await _postService.DeletePost(id);

                    SuccessResponse resp = new ()
                    {
                        Code = 0,
                        Id = post.Id.ToString(),
                        Name = post.PostName,
                        InfoMessage = "Статья успешно удалена",
                    };

                    _logger.LogInformation("Статья удалена: " + post.PostName);
                    return Json(resp);
                }

                _logger.LogInformation("Автор статьи не авторизован или недостаточно прав");
                return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован или недостаточно прав", ErrorCode = 40004 }).Value);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Произошла ошибка при удалении статьи " + ex.Message);
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Произошла ошибка при удалении статьи", ErrorCode = 40003 }).Value);
            }
        }

        /// <summary>
        /// добавление тега к статье.
        /// </summary>
        /// <param name="tagid"> id тега.</param>
        /// <param name="postid"> id статьи.</param>
        /// <response code="200">Тег добавлен.</response>
        /// <response code="400">не удалось добавить тег.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом добавления, JSON.</returns>
        // GET: TagController/Create
        [HttpPost]
        [Route("{postid}/tag/{tagid}")]
        public async Task<IActionResult> AddTag([FromRoute] int tagid, [FromRoute] int postid)
        {
            var post = await _postService.GetPostById(postid);

            var tag = await _tagService.GetTagById(tagid);
            bool needAdd = true;
            foreach (var tagpost in post.Tags)
            {
                if (tag.TagText == tagpost.TagText)
                {
                    needAdd = false;
                    break;
                }
            }

            if (needAdd)
            {
                post.Tags.Add(tag);

                await _postService.EditPost(postid, post);

                _logger.LogInformation("К статье " + post.Id + " добавлен тег " + tag.TagText);

                SuccessResponse resp = new ()
                {
                    Code = 0,
                    InfoMessage = "К статье " + post.Id + " добавлен тег " + tag.TagText,
                };

                return Json(resp);
            }

            _logger.LogInformation("Тег уже добавлен");
            return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег уже добавлен", ErrorCode = 40003 }).Value);
        }

        /// <summary>
        /// Удалить тег у статьи.
        /// </summary>
        /// <param name="tagid"> id тега.</param>
        /// <param name="postid"> id статьи.</param>
        /// <response code="200">Тег удален.</response>
        /// <response code="400">не удалось удалить тег.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение об удалении тега в формате JSON.</returns>
        // GET: TagController/Create
        [HttpDelete]
        [Route("{postid}/tag/{tagid}")]
        public async Task<IActionResult> RemoveTag(int tagid, int postid)
        {
            var post = await _postService.GetPostById(postid);
            var tag = await _tagService.GetTagById(tagid);
            post.Tags.Remove(tag);
            await _postService.EditPost(postid, post);

            _logger.LogInformation("У статьи " + post.Id + " Удален тег " + tag.TagText);

            SuccessResponse resp = new()
            {
                Code = 0,
                InfoMessage = "У статьи " + post.Id + " Удален тег " + tag.TagText,
            };

            return Json(resp);
        }
    }
}
