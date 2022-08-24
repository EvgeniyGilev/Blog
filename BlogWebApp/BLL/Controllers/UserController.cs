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
using Microsoft.AspNetCore.Authorization;

namespace BlogWebApp.BLL.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        //получить всех пользователей
        // GET: UserController
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repo.GetUsers();

            return View(users);
        }

        //получить одного пользователя
        // GET: UserController
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.GetUserById (id);

            return View(user);
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
        public async Task<IActionResult> Register([FromForm] User newUser)
        {            
            await _repo.AddUser(newUser);
            return RedirectToAction("GetAllUsers");
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit()
        {
            return View();
        }

        //отредактировать пользователя
        // GET: UserController/Edit/5
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromForm] User newUser)
        {
            await _repo.EditUser(newUser);
            return RedirectToAction("GetAllUsers");
        }


        // POST: UserController/Delete/Id
        [HttpPost]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {

            var user = await _repo.GetUserById(id);
            if (user == null) { return RedirectToAction(nameof(Index)); }
            else
            {
                await _repo.DelUser(user);
                return RedirectToAction("GetAllUsers");
            }
        }

        [HttpGet]
        [Route("Authenticate")]
        public IActionResult Authenticate()
        {
            return View();
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) ||
              String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User? user = _repo.GetUserByLogin(login);
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.UserPassword != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserLogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(";",user.Roles))
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return View(user);
        }
    }
}
