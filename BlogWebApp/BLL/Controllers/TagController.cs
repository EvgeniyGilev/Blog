using Microsoft.AspNetCore.Mvc;
using BlogWebApp.BLL.Models.ViewModels.TagViews;
using BlogWebApp.Handlers;
using BlogAPI.DATA.Models;
using BlogWebApp.BLL.Interfaces.Services;

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

        private readonly ITagService _tagService;
        private readonly ILogger<TagController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="logger">The logger.</param>
        public TagController(ITagService tagService, ILogger<TagController> logger)
        {
            _tagService = tagService;
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
            var tags = await _tagService.ListAsync();
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
            var tag = await _tagService.GetTagById(id);
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
            var searchtag = await _tagService.CreateTag(newTag.tagText);
            if (searchtag == true)
            {
                await _tagService.CreateTag(newTag.tagText);
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
            var tag = await _tagService.GetTagById(id);

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
            var tag = await _tagService.GetTagById(id);
            tag.tagText = newTag.tagText;

            await _tagService.EditTag(id, tag);
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

            var tag = await _tagService.GetTagById(id);
            if (tag == null) { return RedirectToAction("Error404", "Error"); }
            else
            {
                await _tagService.DeleteTag(id);
                _logger.LogInformation("Удалили тег по id: " + id.ToString() + " имя тега: " + tag.tagText);
                return RedirectToAction("GetTags");
            }
        }
    }
}
