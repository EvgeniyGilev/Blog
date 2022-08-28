using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogWebApp.BLL.Models;
using AutoMapper;
using BlogWebApp.DAL.Repositories.Interfaces;
using BlogWebApp.BLL.Models.Entities;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BlogWebApp.BLL.Models.ViewModels;

namespace BlogWebApp.BLL.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// получить всех пользователей
        /// </summary>
        /// <returns></returns>
        // GET: UserController
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = _userManager.Users.ToList();

            List<ShowUserViewModel> model = new();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                List<string> roles = new();

                foreach (var role in userRoles)
                {
                    roles.Add(role.ToString());
                }

                model.Add(new ShowUserViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserName = user.UserFirstName + " " + user.UserLastName,
                    UserRoles = roles
                });
            }
            return View(model);
        }

        /// <summary>
        /// получить одного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserController
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            EditUserViewModel model = new()
            {
                Id = user.Id,
                Email = user.Email,
                UserFirstName = user.UserFirstName,
                UserLastName = user.UserLastName,
                UserPassword = user.UserPassword
            };

            return View(model);
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// зарегистрировать пользователя
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        // POST: UserController/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] CreateUserViewModel newUser)
        {
            User user = new()
            {
                Email = newUser.Email,
                UserName = newUser.Email,
                UserCreateDate = DateTime.Now.ToString(),
                UserFirstName = newUser.UserFirstName,
                UserLastName = newUser.UserLastName,
                UserPassword = newUser.UserPassword
            };


            //по умолчанию права пользователя

            var result = await _userManager.CreateAsync(user, newUser.UserPassword);
            if (result.Succeeded)
            {
                //добавляем роль по умолчанию Пользователь
                await _userManager.AddToRoleAsync(user, "Пользователь");
                if (!User.Identity.IsAuthenticated)
                {
                    await _signInManager.SignInAsync(user, false);
                }

                // await _repo.AddUser(newUser);
            }
            return RedirectToAction("GetAllUsers");
        }

        /// <summary>
        /// отредактировать пользователя
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        // GET: UserController/Edit/5
        [HttpPost]
        [Route("Edit/{Id}")]
        public async Task<IActionResult> Edit([FromForm] EditUserViewModel newUser, [FromRoute] string Id)
        {
            //await _repo.EditUser(newUser, Id);

            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    user.Email = newUser.Email;
                    user.UserName = newUser.Email;

                    user.UserLastName = newUser.UserLastName;
                    user.UserFirstName = newUser.UserFirstName;
                    user.UserPassword = newUser.UserPassword;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAllUsers");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return RedirectToAction("GetAllUsers");
        }


        /// <summary>
        /// POST: UserController/Delete/Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {

            if (User.IsInRole("Администратор"))
            {


                var user = await _userManager.FindByIdAsync(id); //await _repo.GetUserById(id);
                if (user == null) { return RedirectToAction(nameof(Index)); }
                else
                {
                    // await _repo.DelUser(user);
                    await _userManager.DeleteAsync(user);
                    return RedirectToAction("GetAllUsers");
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }

        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                        return View();
                    }
                }
            }
           
            return RedirectToAction("Index", "Home");
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            return RedirectToAction("Index", "Home");
        }


    }
}
