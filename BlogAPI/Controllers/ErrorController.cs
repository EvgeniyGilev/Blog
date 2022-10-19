// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using BlogAPI.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    /// <summary>
    /// The error controller.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ILogger<CommentController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        /// <param name="logger"> NLOG logger.</param>
        public ErrorController(ILogger<CommentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Страница не найдена.
        /// </summary>
        /// <returns>404 not found, JSON.</returns>
        [HttpGet]
        [Route("/Error/Error404")]
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            _logger.LogError("404 not found");

            return Json(new ErrorResponse { ErrorMessage = "404 not found" });
        }

        /// <summary>
        /// Доступ запрещен.
        /// </summary>
        /// <returns>403 not found,JSON.</returns>
        [HttpGet]
        [Route("/Error/Error403")]
        public ActionResult Error403()
        {
            Response.StatusCode = 403;
            _logger.LogError("403 Forbidden");
            return Json(new ErrorResponse { ErrorMessage = "403 not found" });
        }

        /// <summary>
        /// Внутренняя ошибка.
        /// </summary>
        /// <returns>500 not found, JSON.</returns>
        [HttpGet]
        [Route("/Error/Error500")]
        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            _logger.LogError("500 internal error");
            return Json(new ErrorResponse { ErrorMessage = "500 not found" });
        }

        /// <summary>
        /// Внутренняя ошибка.
        /// </summary>
        /// <returns>500 not found, JSON.</returns>
        [HttpGet]
        [Route("/Error/Error")]
        public ActionResult Error()
        {
            Response.StatusCode = 500;
            _logger.LogError("Ошибка что-то пошло не так");
            return Json(new ErrorResponse { ErrorMessage = "500 not found" });
        }
    }
}
