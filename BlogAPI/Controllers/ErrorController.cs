using BlogAPI.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {

        private readonly ILogger<CommentController> _logger;

        public ErrorController (ILogger<CommentController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Страница не найдена
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Error/Error404")]
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            _logger.LogError("404 not found");

            return Json(new ErrorResponse { ErrorMessage = "404 not found" });
        }
        /// <summary>
        /// Доступ запрещен
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Error/Error403")]
        public ActionResult Error403()
        {
            Response.StatusCode = 403;
            _logger.LogError("403 Forbidden");
            return Json(new ErrorResponse { ErrorMessage = "403 not found" });
        }
        /// <summary>
        /// Внутренняя ошибка
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Error/Error500")]
        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            _logger.LogError("500 internal error");
            return Json(new ErrorResponse { ErrorMessage = "500 not found" });
        }
        /// <summary>
        /// Внутренняя ошибка
        /// </summary>
        /// <returns></returns>
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
