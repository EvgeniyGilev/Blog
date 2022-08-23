using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {

        private readonly ITagRepository _repo;

        public RoleController(ITagRepository repo)
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
        public async Task<IActionResult> Create(Tag newTag)
        {
            await _repo.CreateTag(newTag);
            return View(newTag);
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit()
        {
            return View();
        }

        // GET: TagController/Edit
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Tag newTag)
        {
            await _repo.EditTag(newTag);
            return View(newTag);
        }

        // GET: TagController/Delete/5
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var tag = await _repo.GetTagById(id);
            if (tag == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelTag(tag);
                return View(tag);
            }
        }
    }
}
