using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BlogWebApp.BLL.Models.ViewModels.TagViews;
using BlogWebApp.BLL.Models;
using System.Diagnostics;

namespace BlogWebApp.BLL.Controllers
{
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
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            _logger.LogError("404 not found");
            return View();
        }
        /// <summary>
        /// Доступ запрещен
        /// </summary>
        /// <returns></returns>
        public ActionResult Error403()
        {
            Response.StatusCode = 403;
            _logger.LogError("403 Forbidden");
            return View();
        }
        /// <summary>
        /// Внутренняя ошибка
        /// </summary>
        /// <returns></returns>
        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            _logger.LogError("500 internal error");
            return View();
        }
        /// <summary>
        /// Внутренняя ошибка
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            Response.StatusCode = 500;
            _logger.LogError("Ошибка что-то пошло не так");
            return View();
        }


    }
}
