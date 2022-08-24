using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create([FromForm] Tag newTag)
        {
            await _repo.CreateTag(newTag);
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
        public async Task<IActionResult> Edit([FromForm] Tag newTag, [FromRoute] int id)
        {
            await _repo.EditTag(newTag,id);
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
