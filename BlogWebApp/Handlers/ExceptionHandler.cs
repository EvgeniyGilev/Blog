using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Handlers
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

            if (!context.ExceptionHandled)
            {
                context.Result = new RedirectResult("/Error/Error");
                context.ExceptionHandled = true;
            }
        }
    }
}