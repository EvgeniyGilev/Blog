// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using AutoMapper;
using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Tags;
using BlogAPI.DATA.Models;
using BlogAPI.Handlers;
using BlogAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    /// <summary>
    /// Действия с тегами.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("api/tag/")]
    public class TagController : Controller
    {
        private readonly ILogger<TagController> _logger;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        /// /// <param name="tagService"> бизнес логика работы с tag</param>
        public TagController(ILogger<TagController> logger, IMapper mapper, ITagService tagService)
        {
            _logger = logger;
            _mapper = mapper;
            _tagService = tagService;
        }

        /// <summary>
        /// Получить все теги.
        /// </summary>
        /// <response code="200">Теги выведены.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает все теги, JSON.</returns>
        // GET: TagController
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.ListAsync();

            _logger.LogInformation("Получили все теги");

            var enumerable = tags.ToList();
            GetTags resp = new ()
            {
                Count = enumerable.Count,
                Tags = _mapper.Map<IEnumerable<Tag>, List<TagView>>(enumerable),
            };

            return Json(resp);
        }

        /// <summary>
        /// Получить тег по id.
        /// </summary>
        /// <param name="id"> ID (int) тега.</param>
        /// <response code="200">Тег выведен.</response>
        /// <response code="400">Тег не найден.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает тег,JSON.</returns>
        // GET: TagController
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTagById([FromRoute] int id)
        {
            var tag = await _tagService.GetTagById(id);
            TagView resp = new()
            {
                id = id,
                tagText = tag.TagText,
            };

            _logger.LogInformation("Получили тег по id: " + id.ToString() + " имя тега: " + tag.TagText);
            return Json(resp);
        }

        /// <summary>
        /// Создание нового тега.
        /// </summary>
        /// <param name="newTag"> название тега.</param>
        /// <response code="200">Тег создан.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом добавления, JSON.</returns>
        // GET: TagController/Create
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm] CreateTagModel newTag)
        {
            if (User.IsInRole("Администратор"))
            {
                try
                {
                    var isTagCreate = await _tagService.CreateTag(newTag.tagText);
                    if (isTagCreate)
                    {
                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            InfoMessage = "Новый тег успешно создан - " + newTag.tagText,
                        };
                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogInformation("Тег уже существует");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег уже существует", ErrorCode = 40003 }).Value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Произошла ошибка при создании нового тега " + ex.Message);
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Произошла ошибка при создании нового тега", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Редактируем тег по его id.
        /// </summary>
        /// <param name="newTag"> название тега.</param>
        /// <param name="id">Id (int) тега.</param>
        /// <response code="200">Тег отредактирован.</response>
        /// <response code="400">Тег не найден.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом редактирования, JSON.</returns>
        // GET: TagController/Edit
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit([FromForm] EditeTagModel newTag, [FromRoute] int id)
        {
            if (User.IsInRole("Администратор"))
            {
                try
                {
                    var isTagEdite = await _tagService.EditTag(id, new Tag(newTag.tagText));
                    if (isTagEdite)
                    {
                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            Id = id.ToString(),
                            Name = newTag.tagText,
                            InfoMessage = "Тег успешно изменен",
                        };

                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogInformation("Тег не найден");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег не найден", ErrorCode = 40003 }).Value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Произошла ошибка при редактировании тега " + ex.Message);
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Произошла ошибка при редактировании тега", ErrorCode = 40003 }).Value);
                }
            }
            else
            {
                _logger.LogWarning("Доступ запрещен");
                return StatusCode(403, Json(new ErrorResponse { ErrorMessage = "Доступ запрещен, нужны права Администратора", ErrorCode = 40011 }).Value);
            }
        }

        /// <summary>
        /// Удаляем тег по его id.
        /// </summary>
        /// <param name="id">Id (int) тега.</param>
        /// <response code="200">Тег удален.</response>
        /// <response code="400">Тег не найден.</response>
        /// <response code="500">Произошла непредвиденная ошибка.</response>
        /// <returns>Возвращает сообщение со статусом удаления, JSON.</returns>
        // GET: TagController/Delete/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (User.IsInRole("Администратор"))
            {
                try
                {
                    var isTagDelete = await _tagService.DeleteTag(id);
                    if (isTagDelete)
                    {
                        SuccessResponse resp = new ()
                        {
                            Code = 0,
                            Id = id.ToString(),
                            InfoMessage = "Тег успешно удален",
                        };

                        return Json(resp);
                    }
                    else
                    {
                        _logger.LogInformation("Тег не найден");
                        return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег не найден", ErrorCode = 40003 }).Value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Произошла ошибка при удалении тега " + ex.Message);
                    return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Произошла ошибка при удалении тега", ErrorCode = 40003 }).Value);
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
