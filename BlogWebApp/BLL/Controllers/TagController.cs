using Microsoft.AspNetCore.Mvc;
using BlogWebApp.BLL.Models.ViewModels.TagViews;
using BlogWebApp.Handlers;
using BlogAPI.DATA.Repositories.Interfaces;
using BlogAPI.DATA.Models;

namespace BlogWebApp.BLL.Controllers
{
    /// <summary>
    /// The tag controller.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class TagController : Controller
    {

        private readonly ITagRepository _repo;
        private readonly ILogger<TagController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="logger">The logger.</param>
        public TagController(ITagRepository repo, ILogger<TagController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        /// <summary>
        ///  получить все теги.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        // GET: TagController
        [HttpGet]
        [Route("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _repo.GetTags();
            _logger.LogInformation("Получили все теги");
            return View(tags);
        }

        /// <summary>
        /// получить теги по id.
        /// </summary>
        /// <param name="id">id тега.</param>
        /// <returns>An IActionResult.</returns>
        // GET: TagController
        [HttpGet]
        [Route("GetTagById")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _repo.GetTagById(id);
            _logger.LogInformation("Получили тег по id: " + id.ToString() + " имя тега: " + tag.tagText);
            return View(tag);
        }

        /// <summary>
        /// Creates the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Создание нового тега.
        /// </summary>
        /// <param name="newTag"> форма ввода данных для нового тега</param>
        /// <returns>An IActionResult.</returns>
        // GET: TagController/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreateTagViewModel newTag)
        {
            Tag tag = new Tag(newTag.tagText);
            var searchtag = _repo.GetTagByName(newTag.tagText);
            if (searchtag.Result == null)
            {
                await _repo.CreateTag(tag);
                _logger.LogInformation("Создан новый тег" + tag.tagText);
                return RedirectToAction("GetTags");
            }
            else
            {
                _logger.LogInformation("тег уже существует " + tag.tagText);
                return RedirectToAction("GetTags");
            }
        }

        /// <summary>
        /// Форма редактирования тега по его id получаем текущий тег.
        /// </summary>
        /// <param name="id">id тега.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var tag = await _repo.GetTagById(id);

            EditeTagViewModel model = new EditeTagViewModel
            {
                id = id,
                tagText = tag.tagText,
            };

            _logger.LogInformation("Открыли форму изменения тега по id: " + id.ToString() + " имя тега: " + tag.tagText);
            return View(model);
        }

        /// <summary>
        /// редактируем тег по его id.
        /// </summary>
        /// <param name="newTag">данные тега.</param>
        /// <param name="id">id тега.</param>
        /// <returns>An IActionResult.</returns>
        // GET: TagController/Edit
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] EditeTagViewModel newTag, [FromRoute] int id)
        {
            var tag = await _repo.GetTagById(id);
            tag.tagText = newTag.tagText;

            await _repo.EditTag(tag, id);
            _logger.LogInformation("Изменили тег по id: " + id.ToString() + " новое имя тега: " + tag.tagText);
            return RedirectToAction("GetTags");
        }

        /// <summary>
        /// Удаляем тег по его id.
        /// </summary>
        /// <param name="id">id тега</param>
        /// <returns>An IActionResult.</returns>
        // GET: TagController/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var tag = await _repo.GetTagById(id);
            if (tag == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelTag(tag);
                _logger.LogInformation("Удалили тег по id: " + id.ToString() + " имя тега: " + tag.tagText);
                return RedirectToAction("GetTags");
            }
        }
    }
}
