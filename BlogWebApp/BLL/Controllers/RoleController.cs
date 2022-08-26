using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        private readonly IRoleRepository _repo;

        public RoleController(IRoleRepository repo, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _repo = repo;
            _roleManager = roleManager;
            _userManager = userManager;
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
            var role = await _repo.GetRoleById(id);

            return View(role);
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
        public async Task<IActionResult> Create([FromForm] Role newRole)
        {
            await _repo.CreateRole(newRole);
            await _roleManager.CreateAsync(new IdentityRole { Name = newRole.RoleName, NormalizedName = newRole.RoleName.ToUpper() });
            return RedirectToAction("GetRoles");
        }

        [HttpGet]
        [Route("Edit/{Id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var role = await _repo.GetRoleById(id);

            return View(role);
        }

        // GET: TagController/Edit
        [HttpPost]
        [Route("Edit/{Id}")]
        public async Task<IActionResult> Edit([FromForm] Role newRole, [FromRoute] int id)
        {
            await _repo.EditRole(newRole,id);
            return RedirectToAction("GetRoles");
        }

        // GET: TagController/Delete/5
        [HttpPost]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var role = await _repo.GetRoleById(id);
            if (role == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelRole(role);
                return RedirectToAction("GetRoles");
            }
        }
    }
}
