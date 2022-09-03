using BlogAPI.Contracts.Models;
using BlogAPI.DATA.Models;
using BlogAPI.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogAPI.Controllers
{
    [ExceptionHandler]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Переход на стартовую страницу ");
            return View();
        }

        public IActionResult Logout()
        {
            _logger.LogInformation("Пользователь разлогинился: " + User.Identity.Name);

            //разлогиневаемся и чистим куки
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
             _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }


    }
}