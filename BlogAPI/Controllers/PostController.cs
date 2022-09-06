﻿using AutoMapper;
using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Comments;
using BlogAPI.Contracts.Models.Posts;
using BlogAPI.Contracts.Models.Tags;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BlogAPI.Contracts.Models.Posts.GetPostsModel;

namespace BlogAPI.Controllers
{

    /// <summary>
    /// Действия с статьями блога
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class PostController : Controller
    {

        private readonly IPostRepository _repo;
        private readonly ITagRepository _repotags;
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;

        public PostController(IPostRepository repo, ITagRepository repotags, UserManager<User> userManager, ILogger<PostController> logger, IMapper mapper)
        {
            _repo = repo;
            _repotags = repotags;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить статью по ID
        /// </summary>
        /// <remarks>
        /// Ответ будет в виде JSON (POST)
        /// {
        /// "id": 1,
        /// "postTitle": "Название стати",
        /// "postText": "Текст статьи",
        /// "authorEmail": "example@gmail.com",
        /// "createDate": "2022-09-03T13:40:11"
        /// }
        /// </remarks>
        /// <param name="id"> номер (id) статьи</param>
        /// <response code="200">Получаем статьи</response>
        /// <response code="400">Статьи с таким id не существует!</response>
        /// <response code="500">Произошла ошибка</response>
        // GET: PostController
        [HttpGet]
        [Route("GetPost/{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {

            var post = await _repo.GetPostById(id);
            if (post != null)
            {

                GetPostByIdModel resp = new GetPostByIdModel
                {
                    id = post.id,
                    AuthorEmail = post.User.Email,
                    PostTitle = post.postName,
                    PostText = post.postText,
                    CreateDate = DateTime.Parse(post.postCreateDate)
                };

                _logger.LogInformation("Получаем статью с ID: " + id.ToString());

                return Json(resp);
            }
            else
            {
                _logger.LogInformation("По текущему ID не смогли получить статью" + id.ToString() + "возвращаемся на страницу всех статей.");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
            }
        }

        /// <summary>
        /// Получить статью по ID со всеми комментариями и тегами
        /// </summary>
        /// <param name="id"> номер (id) статьи</param>
        /// <response code="200">Получаем статьи</response>
        /// <response code="400">Статьи с таким id не существует!</response>
        /// <response code="500">Произошла ошибка</response>
        // GET: PostController
        [HttpGet]
        [Route("GetPostFull/{id}")]
        public async Task<IActionResult> GetPostFull([FromRoute] int id)
        {

            var post = await _repo.GetPostById(id);
            if (post != null)
            {

                GetPostFullByIdModel resp = new()
                {
                    id = post.id,
                    AuthorEmail = post.User.Email,
                    PostTitle = post.postName,
                    PostText = post.postText,
                    CreateDate = DateTime.Parse(post.postCreateDate),
                    Tags = _mapper.Map<List<Tag>, List<TagView>>(post.Tags),
                    Comments = _mapper.Map<List<Comment>, List<CommentView>>(post.Comments)

                };

                _logger.LogInformation("Получаем статью с ID: " + id.ToString());

                return Json(resp);
            }
            else
            {
                _logger.LogInformation("По текущему ID не смогли получить статью" + id.ToString() + "возвращаемся на страницу всех статей.");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
            }
        }


        /// <summary>
        /// Получить все статьи
        /// </summary>
        /// <response code="200">Получаем статьи</response>
        /// <response code="400">Возникла ошибка при получении статей</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: PostController
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _repo.GetPosts();
                GetPostsModel resp = new GetPostsModel
                {
                    Count = posts.Length,
                    Posts = _mapper.Map<Post[], PostView[]>(posts)
                };

                _logger.LogInformation("Показываем все статьи, всего статей найдено: " + resp.Count.ToString());

                return Json(resp);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Возникла ошибка при получении статей " + ex.Message);
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Возникла ошибка при получении статей " + ex.Message, ErrorCode = 40002 }).Value);
            }
        }

        /// <summary>
        /// Добавить статью
        /// </summary>
        /// <response code="200">Добавление статью произошло успешно</response>
        /// <response code="400">Добавить статью не удалось, так как автор статьи не найден</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreatePostModel newPost)
        {
            if (User.Identity.IsAuthenticated)
            {
                var searchuser = _userManager.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (searchuser != null)
                {

                    Post post = new Post
                    {
                        postName = newPost.PostName,
                        postText = newPost.PostText,
                        User = searchuser
                    };

                    await _repo.CreatePost(post);
                    _logger.LogInformation("новая статья добавлена: " + post.postName);
                    //Если добавление прошло успешно получим id новой статьи
                    var getpost = (Post)_repo.GetPosts().Result.FirstOrDefault(p => p.postName == post.postName);

                    SuccessResponse resp = new()
                    {
                        code = 0,
                        id = getpost.id.ToString(),
                        name = getpost.postName,
                        infoMessage = "Статья успешно добавлена"
                    };

                    return Json(resp);
                }
                else
                {
                    _logger.LogInformation("Автор статьи не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Автор статьи не найден", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogInformation("Автор статьи не найден");
                return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован", ErrorCode = 40004 }).Value);
            }

        }

        /// <summary>
        /// Редактируем статью по её id
        /// </summary>
        /// <param name="id"> номер (id) статьи</param>
        /// <response code="200">Изменение статьи произошло успешно</response>
        /// <response code="400">Изменить статью не удалось, так как статья не найдена</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        [HttpPatch]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] EditPostModel newPost, [FromRoute] int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var post = await _repo.GetPostById(id);
                if (post != null)
                {
                    if ((User.Identity.Name == post.User.UserName) || User.IsInRole("Администратор"))
                    {
                        post.postName = newPost.PostName;
                        post.postText = newPost.PostText;


                        await _repo.EditPost(post, id);
                        _logger.LogInformation("Статья отредактирована: " + post.postName);


                        SuccessResponse resp = new()
                        {
                            code = 0,
                            id = post.id.ToString(),
                            name = post.postName,
                            infoMessage = "Статья успешно отредактирована"
                        };

                        return Json(resp);
                    }
                    else
                    {
                        return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован или недостаточно прав", ErrorCode = 40004 }).Value);
                    }
                }
                else
                {
                    _logger.LogInformation("Статья не найдена");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
                }

            }
            else
            {
                _logger.LogInformation("Автор статьи не авторизован или недостаточно прав");
                return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован или недостаточно прав", ErrorCode = 40004 }).Value);
            }
        }



        /// <summary>
        /// Удаляем статью по её id
        /// </summary>
        /// <param name="id"> номер (id) статьи</param>
        /// <response code="200">Удаление статьи произошло успешно</response>
        /// <response code="400">Удалить статью не удалось, так как автор статьи не авторизован или недостаточно прав</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // Delete: PostController/Delete/5
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var post = await _repo.GetPostById(id);
            //удалить статью может только автор или администратор
            if ((User.Identity.Name == post.User.UserName) || User.IsInRole("Администратор"))
            {
                if (post == null) { return RedirectToAction(nameof(Index)); }
                else
                {
                    await _repo.DelPost(post);

                    SuccessResponse resp = new()
                    {
                        code = 0,
                        id = post.id.ToString(),
                        name = post.postName,
                        infoMessage = "Статья успешно удалена"
                    };

                    _logger.LogInformation("Статья удалена: " + post.postName);
                    return Json(resp);
                }
            }
            else
            {
                _logger.LogInformation("Автор статьи не авторизован или недостаточно прав");
                return StatusCode(401, Json(new ErrorResponse { ErrorMessage = "Автор статьи не авторизован или недостаточно прав", ErrorCode = 40004 }).Value);
            }

        }

        /// <summary>
        /// добавление тега к статье
        /// </summary>
        /// <param name="tagid"> id тега</param>
        /// <param name="postid"> id статьи</param>
        /// <response code="200">Тег добавлен</response>
        /// <response code="400">не удалось добавить тег</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: TagController/Create
        [HttpPatch]
        [Route("AddTag")]
        public async Task<IActionResult> AddTag(int tagid, int postid)
        {

            var post = await _repo.GetPostById(postid);
            if (post != null)
            {
                var tag = await _repotags.GetTagById(tagid);
                if (tag != null)
                {

                    bool needAdd = true;
                    foreach (var tagpost in post.Tags)
                    {
                        if (tag.tagText == tagpost.tagText)
                        {
                            needAdd = false;
                            break;
                        }
                    }
                    if (needAdd)
                    {

                        post.Tags.Add(tag);

                        await _repo.EditPost(post, postid);

                        _logger.LogInformation("К статье " + post.id.ToString() + " добавлен тег " + tag.tagText);

                        SuccessResponse resp = new()
                        {
                            code = 0,
                            infoMessage = "К статье " + post.id.ToString() + " добавлен тег " + tag.tagText
                        };

                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogInformation("Тег уже добавлен");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег уже добавлен", ErrorCode = 40003 }).Value);
                    }
                }
                else
                {
                    _logger.LogInformation("Тег не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег не найден", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogInformation("Статья не найдена");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
            }
        }

        /// <summary>
        /// удалить тег у статьи
        /// </summary>
        /// <param name="tagid"> id тега</param>
        /// <param name="postid"> id статьи</param>
        /// <response code="200">Тег удален</response>
        /// <response code="400">не удалось удалить тег</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: TagController/Create
        [HttpPatch]
        [Route("RemoveTag")]
        public async Task<IActionResult> RemoveTag(int tagid, int postid)
        {

            var post = await _repo.GetPostById(postid);
            if (post != null)
            {
                var tag = await _repotags.GetTagById(tagid);
                if (tag != null)
                {
                        post.Tags.Remove(tag);
                        await _repo.EditPost(post, postid);

                        _logger.LogInformation("У статьи " + post.id.ToString() + " Удален тег " + tag.tagText);

                        SuccessResponse resp = new()
                        {
                            code = 0,
                            infoMessage = "У статьи " + post.id.ToString() + " Удален тег " + tag.tagText
                        };

                        return Json(resp);
                }
                else
                {
                    _logger.LogInformation("Тег не найден");
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег не найден", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogInformation("Статья не найдена");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Статьи с таким id не существует!", ErrorCode = 40001 }).Value);
            }
        }



    }
}
