﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.BLL.Models.ViewModels.RoleViews;
using BlogWebApp.Handlers;

namespace CustomIdentityApp.Controllers
{
    /// <summary>
    /// The roles controller.
    /// </summary>
    [ExceptionHandler]
    public class RolesController : Controller
    {
        RoleManager<Role> _roleManager;
        UserManager<User> _userManager;
        private readonly ILogger<RolesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager, ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Indices the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        public IActionResult Index()
        {
            _logger.LogInformation("Показываем все доступные роли");
            return View(_roleManager.Roles.ToList());
        }

        /// <summary>
        /// Creates the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        public IActionResult Create() => View();

        /// <summary>
        /// Creates the.
        /// </summary>
        /// <param name="newRole">The new role.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRoleViewModel newRole)
        {
            if (!string.IsNullOrEmpty(newRole.Name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new Role { Name = newRole.Name, Description = newRole.Description });
                if (result.Succeeded)
                {
                    _logger.LogInformation("Новая роль добавлена: " + newRole.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    _logger.LogWarning("Роль не добавлена: " + newRole.Name);
                }
            }

            return View();
        }

        /// <summary>
        /// Deletes the.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                _logger.LogInformation("Роль удалена: " + role.Name);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Users the list.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        public IActionResult UserList()
        {
            _logger.LogInformation("Список пользователей и их прав" );
            return View(_userManager.Users.ToList());
        }

        /// <summary>
        /// Edits the.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A Task.</returns>
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
                    AllRoles = allRoles,
                };

                _logger.LogInformation("Форма изменения ролей пользователя: " + user.Email);
                return View(model);
            }

            return NotFound();
        }

        /// <summary>
        /// Edits the.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roles">The roles.</param>
        /// <returns>A Task.</returns>
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

                _logger.LogInformation("Изменили права пользователя: " + user.Email);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }

        /// <summary>
        /// Edits the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        [HttpGet]
        [Route("EditRole/{id}")]
        public async Task<IActionResult> EditRole([FromRoute] string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                EditeRoleViewModel model = new EditeRoleViewModel
                {
                    Id = id,
                    Name = role.Name,
                    Description = role.Description,
                };

                _logger.LogInformation("Форма изменения роли: " + role.Name);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: TagController/Edit
        /// <summary>
        /// Edits the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="newRole">The new role.</param>
        /// <returns>A Task.</returns>
        [HttpPost]
        [Route("EditRole/{id}")]
        public async Task<IActionResult> EditRole([FromRoute] string id, [FromForm] EditeRoleViewModel newRole)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                role.Name = newRole.Name;
                role.Description = newRole.Description;
                IdentityResult result = await _roleManager.UpdateAsync(role);
                _logger.LogInformation("изменили роль: " + role.Name);
            }

            return RedirectToAction("Index");
        }
    }
}