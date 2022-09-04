using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Contracts.Models;

namespace BlogAPI.Handlers
{
    /// <summary>
    /// обработчик исключений
    /// </summary>
    public class ExceptionHandler : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            var result = new ObjectResult(new
            {
                code = 500,
                message = "Произошла ошибка: ",
                detailedMessage = context.Exception.Message
            });
            result.StatusCode = 500;
            context.Result = result;

        }




    }
}
