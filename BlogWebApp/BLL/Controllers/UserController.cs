﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using BlogWebApp.BLL.Models.ViewModels.UserViews;
using BlogWebApp.Handlers;
using BlogAPI.DATA.Models;

namespace BlogWebApp.BLL.Controllers
{
    /// <summary>
    /// The user controller.
    /// </summary>
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="logger">The logger.</param>
        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<UserController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// получить всех пользователей.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        // GET: UserController
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = _userManager.Users.ToList();

            List<ShowUserViewModel> model = new ();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                List<string> roles = new ();

                foreach (var role in userRoles)
                {
                    roles.Add(role);
                }

                model.Add(new ShowUserViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserName = user.UserFirstName + " " + user.UserLastName,
                    UserRoles = roles,
                });
            }

            _logger.LogInformation("Форма отображения всех пользователей, всего пользователей: " + users.Count.ToString());
            return View(model);
        }

        /// <summary>
        /// получить одного пользователя.
        /// </summary>
        /// <param name="id">id пользователя.</param>
        /// <returns>An IActionResult.</returns>
        // GET: UserController
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            EditUserViewModel model = new ()
            {
                Id = user.Id,
                Email = user.Email,
                UserFirstName = user.UserFirstName,
                UserLastName = user.UserLastName,
            };
            _logger.LogInformation("Форма редактирования пользователя по его id: " + id + " Email: " + user.UserName);
            return View(model);
        }

        /// <summary>
        /// Создание пользователя (регистрация).
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// зарегистрировать пользователя.
        /// </summary>
        /// <param name="newUser">данные пользователя.</param>
        /// <returns>An IActionResult.</returns>
        // POST: UserController/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] CreateUserViewModel newUser)
        {
            User user = new()
            {
                Email = newUser.Email,
                UserName = newUser.Email,
                UserCreateDate = DateTime.Now.ToString(CultureInfo.CurrentCulture),
                UserFirstName = newUser.UserFirstName,
                UserLastName = newUser.UserLastName,
            };

            // по умолчанию права пользователя
            var result = await _userManager.CreateAsync(user, newUser.UserPassword);
            if (result.Succeeded)
            {
                // добавляем роль по умолчанию Пользователь
                await _userManager.AddToRoleAsync(user, "Пользователь");
                if (User.Identity is { IsAuthenticated: false })
                {
                    await _signInManager.SignInAsync(user, false);
                }

                _logger.LogInformation("Пользователь зарегистрирован Email: " + user.UserName);
            }

            return RedirectToAction("GetAllUsers");
        }

        /// <summary>
        /// отредактировать пользователя по его id.
        /// </summary>
        /// <param name="newUser">данные пользователя.</param>
        /// <param name="id">id пользователя.</param>
        /// <returns>An IActionResult.</returns>
        // GET: UserController/Edit/5
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] EditUserViewModel newUser, [FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Email = newUser.Email;
                    user.UserName = newUser.Email;

                    user.UserLastName = newUser.UserLastName;
                    user.UserFirstName = newUser.UserFirstName;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newUser.UserPassword);

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Пользователь отредактирован Email: " + user.UserName);
                        return RedirectToAction("GetAllUsers");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        _logger.LogWarning("При редактировании пользователя возникли ошибки " + user.UserName);
                    }
                }
            }

            return RedirectToAction("GetAllUsers");
        }

        /// <summary>
        /// POST: UserController/Delete/Id.
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (User.IsInRole("Администратор"))
            {
                var user = await _userManager.FindByIdAsync(id);

                // Удаляем самого пользователя
                await _userManager.DeleteAsync(user);
                _logger.LogInformation("Пользователь удален Email: " + user.UserName);
                return RedirectToAction("GetAllUsers");
            }

            return Redirect("~/Error/Error403");
        }

        /// <summary>
        /// Logins the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="user">данные пользователя.</param>
        /// <returns>An IActionResult.</returns>
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var searchuser = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);

                if (searchuser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Пользователь успешно залогинился Email: " + user.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                        _logger.LogWarning("Неправильный логин и(или) пароль");
                        return View();
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logouts the.
        /// </summary>
        /// <returns>A Task.</returns>
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            _logger.LogInformation("Пользователь успешно вышел Email: " + username);

            return RedirectToAction("Index", "Home");
        }
    }
}
