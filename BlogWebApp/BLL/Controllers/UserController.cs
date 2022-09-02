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
using BlogWebApp.BLL.Models.ViewModels.UserViews;
using BlogWebApp.Handlers;

namespace BlogWebApp.BLL.Controllers
{
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IPostRepository _repoposts;
        private readonly ICommentRepository _repocomments;


        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<UserController> logger, IPostRepository repoposts, ICommentRepository repocomments)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _repoposts = repoposts;
            _repocomments = repocomments;
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
            _logger.LogInformation("Форма отображения всех пользователей, всего пользователей: " + users.Count.ToString() );
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
            _logger.LogInformation("Форма редактирования пользователя по его id: " + id + " Email: " +user.UserName);
            return View(model);
        }
        /// <summary>
        /// Создание пользователя (регистрация)
        /// </summary>
        /// <returns></returns>
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
                _logger.LogInformation("Пользователь зарегистрирован Email: " + user.UserName);
                // await _repo.AddUser(newUser);
            }
            return RedirectToAction("GetAllUsers");
        }

        /// <summary>
        /// отредактировать пользователя по его id
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        // GET: UserController/Edit/5
        [HttpPost]
        [Route("Edit/{id}")]
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
        /// POST: UserController/Delete/Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {

            if (User.IsInRole("Администратор"))
            {
                var user = await _userManager.FindByIdAsync(id); //await _repo.GetUserById(id);
                if (user == null) { return RedirectToAction(nameof(Index)); }
                else
                {
                    //Если удаляем пользователя то нужно решать что делать с его Статьями и его комментариями.
                    //В лоб удаляем статьи пользователя
                   /* if (user.Posts != null)
                    {
                        foreach (var post in user.Posts)
                        {
                            await _repoposts.DelPost(post);
                            _logger.LogInformation("Удаление статьей пользователя, id статьи: " + post.id + " название " + post.postName);
                        }
                    }
                    // в лоб удаляем комментарии
                    if (user.Comments != null)
                    {
                        foreach (var comment in user.Comments)
                        {
                            await _repocomments.DelComment(comment);
                            _logger.LogInformation("Удаляем комментарий пользователя, id комментария: " + comment.id);
                        }
                    }
                   */
                    // Удаляем самого пользователя
                    await _userManager.DeleteAsync(user);
                    _logger.LogInformation("Пользователь удален Email: " + user.UserName);
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

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity.Name;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            _logger.LogInformation("Пользователь успешно вышел Email: " + username);

            return RedirectToAction("Index", "Home");
        }


    }
}
