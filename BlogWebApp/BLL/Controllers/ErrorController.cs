// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.BLL.Controllers
{

    /// <summary>
    /// The error controller.
    /// </summary>
    public class ErrorController : Controller
    {
        private readonly ILogger<CommentController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ErrorController (ILogger<CommentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Страница не найдена.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            _logger.LogError("404 not found");
            return View();
        }

        /// <summary>
        /// Доступ запрещен.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        public ActionResult Error403()
        {
            Response.StatusCode = 403;
            _logger.LogError("403 Forbidden");
            return View();
        }

        /// <summary>
        /// Внутренняя ошибка.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            _logger.LogError("500 internal error");
            return View();
        }

        /// <summary>
        /// Внутренняя ошибка.
        /// </summary>
        /// <returns>An ActionResult.</returns>
        public ActionResult Error()
        {
            Response.StatusCode = 500;
            _logger.LogError("Ошибка что-то пошло не так");
            return View();
        }
    }
}
