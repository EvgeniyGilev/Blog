using AutoMapper;
using BlogAPI.Contracts.Models;
using BlogAPI.Contracts.Models.Tags;
using BlogAPI.DATA.Models;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    /// <summary>
    /// Действия с тегами
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class TagController : Controller
    {

        private readonly ITagRepository _repo;
        private readonly ILogger<TagController> _logger;
        private IMapper _mapper;

        public TagController(ITagRepository repo, ILogger<TagController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все теги
        /// </summary>
        /// <response code="200">Теги выведены</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: TagController
        [HttpGet]
        [Route("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _repo.GetTags();
           
            _logger.LogInformation("Получили все теги");

            GetTags resp = new()
            {
                Count = tags.Count,
                Tags = _mapper.Map<List<Tag>, List<TagView>>(tags)
            };

            return Json(resp);
        }


        /// <summary>
        /// Получить теги по id
        /// </summary>
        /// <param name="id"> ID (int) тега </param>
        /// <response code="200">Тег выведен</response>
        /// <response code="400">Тег не найден </response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: TagController
        [HttpGet]
        [Route("GetTagById")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _repo.GetTagById(id);
            if (tag != null)
            {
                TagView resp = new()
                {
                    id = id,
                    tagText = tag.tagText
                };

                _logger.LogInformation("Получили тег по id: " + id.ToString() + " имя тега: " + tag.tagText);
                return Json(resp);
            }
            else
            {
                _logger.LogInformation("Тег не найден");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег не найден", ErrorCode = 40003 }).Value);
            }
        }

        /// <summary>
        /// Создание нового тега
        /// </summary>
        /// <param name="newTag"> название тега</param>
        /// <response code="200">Тег создан</response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: TagController/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreateTagModel newTag)
        {
            var tag = _repo.GetTagByName(newTag.tagText);
            if (tag.Result == null)
            {
                await _repo.CreateTag(new Tag(newTag.tagText));
                _logger.LogInformation("Создан новый тег" + newTag.tagText);

                SuccessResponse resp = new()
                {
                    code = 0,
                    infoMessage = "Новый тег успешно создан - " + newTag.tagText
                };

                return Json(resp);
            }
            else
            {
                _logger.LogInformation("Тег уже существует");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег уже существует", ErrorCode = 40003 }).Value);
            }
        }

        /// <summary>
        /// Редактируем тег по его id
        /// </summary>
        /// <param name="newTag"> название тега</param>
        /// <param name="id">Id (int) тега</param>
        /// <response code="200">Тег отредактирован</response>
        /// <response code="400">Тег не найден </response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: TagController/Edit
        [HttpPatch]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] EditeTagModel newTag, [FromRoute] int id)
        {
            var tag = await _repo.GetTagById(id);
            if (tag != null)
            {
                tag.tagText = newTag.tagText;

                await _repo.EditTag(tag, id);
                _logger.LogInformation("Изменили тег по id: " + id.ToString() + " новое имя тега: " + tag.tagText);

                SuccessResponse resp = new()
                {
                    code = 0,
                    id= id.ToString(),
                    name = tag.tagText,
                    infoMessage = "Тег успешно изменен"
                };

                return Json(resp);


            }
            else
            {
                _logger.LogInformation("Тег не найден");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег не найден", ErrorCode = 40003 }).Value);
            }
        }

        /// <summary>
        /// Удаляем тег по его id
        /// </summary>
        /// <param name="id">Id (int) тега</param>
        /// <response code="200">Тег удален</response>
        /// <response code="400">Тег не найден </response>
        /// <response code="500">Произошла непредвиденная ошибка</response>
        // GET: TagController/Delete/5
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {

            var tag = await _repo.GetTagById(id);
            if (tag !=null)
           
            {
                await _repo.DelTag(tag);
                _logger.LogInformation("Удалили тег по id: " + id.ToString() + " имя тега: " + tag.tagText);
                
                SuccessResponse resp = new()
                {
                    code = 0,
                    id = id.ToString(),
                    name = tag.tagText,
                    infoMessage = "Тег успешно удален"
                };

                return Json(resp);
            }
            else
            {
                _logger.LogInformation("Тег не найден");
                return StatusCode(400, Json(new ErrorResponse { ErrorMessage = "Тег не найден", ErrorCode = 40003 }).Value);
            }
        }
    }
}
