// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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
        /// <param name="tagService">The tagService.</param>
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
            var tag = new Tag(newTag.TagText);
            var searchtag = await _tagService.CreateTag(newTag.TagText);
            if (searchtag)
            {
                await _tagService.CreateTag(newTag.TagText);
                _logger.LogInformation("Создан новый тег" + tag.TagText);
                return RedirectToAction("GetTags");
            }
            else
            {
                _logger.LogInformation("тег уже существует " + tag.TagText);
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
                Id = id,
                TagText = tag.TagText,
            };

            _logger.LogInformation("Открыли форму изменения тега по id: " + id.ToString() + " имя тега: " + tag.TagText);
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
            tag.TagText = newTag.TagText;

            await _tagService.EditTag(id, tag);
            _logger.LogInformation("Изменили тег по id: " + id.ToString() + " новое имя тега: " + tag.TagText);
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
            await _tagService.DeleteTag(id);
            _logger.LogInformation("Удалили тег по id: " + id + " имя тега: " + tag.TagText);
            return RedirectToAction("GetTags");
        }
    }
}
