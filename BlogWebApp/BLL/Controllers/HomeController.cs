// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using Microsoft.AspNetCore.Mvc;
using BlogWebApp.BLL.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using BlogWebApp.Handlers;
using BlogAPI.DATA.Models;

namespace BlogWebApp.BLL.Controllers
{
    /// <summary>
    /// The home controller.
    /// </summary>
    [ExceptionHandler]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Indices the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        public IActionResult Index()
        {
            _logger.LogInformation("Переход на стартовую страницу ");
            return View();
        }

        /// <summary>
        /// Logouts the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        public IActionResult Logout()
        {
            _logger.LogInformation("Пользователь разлогинился: " + User.Identity?.Name);

            // разлогиневаемся и чистим куки
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Privacies the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Errors the.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Возникла ошибка");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}