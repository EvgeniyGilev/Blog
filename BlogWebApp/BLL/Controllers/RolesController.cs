using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.BLL.Models.ViewModels.RoleViews;

namespace CustomIdentityApp.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<Role> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRoleViewModel newRole)
        {
            if (!string.IsNullOrEmpty(newRole.Name))
            {
      
                IdentityResult result = await _roleManager.CreateAsync(new Role { Name = newRole.Name, Description = newRole.Description });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получаем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получаем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
        [HttpGet]
        [Route("EditRole/{Id}")]
        public async Task<IActionResult> EditRole([FromRoute] string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                EditeRoleViewModel model = new EditeRoleViewModel
                {
                    Id = id,
                    Name = role.Name,
                    Description = role.Description
                };

                return View(model);
            }

            else return RedirectToAction("Index");
        }

        // GET: TagController/Edit
        [HttpPost]
        [Route("EditRole/{Id}")]
        public async Task<IActionResult> EditRole([FromRoute] string id, [FromForm] EditeRoleViewModel newRole)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                role.Name = newRole.Name;
                role.Description = newRole.Description;
                IdentityResult result = await _roleManager.UpdateAsync(role);
            }
            return RedirectToAction("Index");
        }

    }
}