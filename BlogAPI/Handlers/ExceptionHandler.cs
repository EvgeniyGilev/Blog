using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Handlers
{
    /// <summary>
    /// обработчик исключений.
    /// </summary>
    public class ExceptionHandler : Attribute, IExceptionFilter
    {
        /// <summary>
        /// Ons the exception.
        /// </summary>
        /// <param name="context">The context.</param>
        public void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                code = 500,
                message = "Произошла ошибка: ",
                detailedMessage = context.Exception.Message,
            });
            result.StatusCode = 500;
            context.Result = result;
        }
    }
}
