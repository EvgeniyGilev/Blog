using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {

        private readonly IRoleRepository _repo;

        public RoleController(IRoleRepository repo)
        {
            _repo = repo;
        }

        //получить все комментарии
        // GET: TagController
        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _repo.GetRoles();
            return View(roles);
        }


        //получить комментарий по id
        // GET: TagController
        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var tag = await _repo.GetRoleById(id);

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
        public async Task<IActionResult> Create(Role newRole)
        {
            await _repo.CreateRole(newRole);
            return View(newRole);
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
        public async Task<IActionResult> Edit(Role newRole)
        {
            await _repo.EditRole(newRole);
            return View(newRole);
        }

        // GET: TagController/Delete/5
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var role = await _repo.GetRoleById(id);
            if (role == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelRole(role);
                return View(role);
            }
        }
    }
}
