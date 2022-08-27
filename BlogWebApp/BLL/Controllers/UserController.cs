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
        private readonly IUserRepository _repo;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(IUserRepository repo, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _repo = repo;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //получить всех пользователей
        // GET: UserController
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = _userManager.Users.ToList();

            List<ShowUserViewModel> model = new List<ShowUserViewModel>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                List<string> roles = new List<string>();

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

        //получить одного пользователя
        // GET: UserController
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            EditUserViewModel model = new EditUserViewModel
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

        //зарегистрировать пользователя
        // POST: UserController/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] CreateUserViewModel newUser)
        {
            User user = new User();

            user.Email = newUser.Email;
            user.UserName = newUser.Email;
            user.UserCreateDate = DateTime.Now.ToString();
            user.UserFirstName = newUser.UserFirstName;
            user.UserLastName = newUser.UserLastName;
            user.UserPassword = newUser.UserPassword;


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
        /*
        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit()
        {
            return View();
        }
        */
        //отредактировать пользователя
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


        // POST: UserController/Delete/Id
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
        /*
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromForm] User user)
        {
            User? dbuser = _repo.GetUserByLogin(user.UserLogin);
            if (dbuser is null)
                throw new AuthenticationException("Пользователь на найден");

            if (dbuser.UserPassword != user.UserPassword)
                throw new AuthenticationException("Введенный пароль не корректен");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserLogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(";",user.Roles))
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("index","Home");
        }
        */
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] User user)
        {

            var searchuser = _userManager.Users.FirstOrDefault(u => u.Email == user.Email);

            if (searchuser != null)
            {
                var result = await _signInManager.CanSignInAsync(searchuser);

                if (result)
                {
                    await _signInManager.SignInAsync(searchuser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
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
