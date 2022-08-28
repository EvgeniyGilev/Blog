using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogWebApp.BLL.Models.ViewModels.TagViews;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : Controller
    {

        private readonly ITagRepository _repo;

        public TagController(ITagRepository repo)
        {
            _repo = repo;
        }

        //получить все комментарии
        // GET: TagController
        [HttpGet]
        [Route("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _repo.GetTags();
            return View(tags);
        }


        //получить комментарий по id
        // GET: TagController
        [HttpGet]
        [Route("GetTagById")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _repo.GetTagById(id);

            return View(tag);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // GET: TagController/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromForm] CreateTagViewModel newTag)
        {
            Tag tag = new Tag(newTag.tagText);
            await _repo.CreateTag(tag);
            return RedirectToAction("GetTags");
        }

        [HttpGet]
        [Route("Edit/{Id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var tag = await _repo.GetTagById(id);

            return View(tag);
        }

        // GET: TagController/Edit
        [HttpPost]
        [Route("Edit/{Id}")]
        public async Task<IActionResult> Edit([FromForm] EditeTagViewModel newTag, [FromRoute] int id)
        {
            var tag = await _repo.GetTagById(id);
            tag.tagText = newTag.tagText;

            await _repo.EditTag(tag, id);
            return RedirectToAction("GetTags");
        }

        // GET: TagController/Delete/5
        [HttpPost]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var tag = await _repo.GetTagById(id);
            if (tag == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelTag(tag);
                return RedirectToAction("GetTags");
            }
        }
    }
}
