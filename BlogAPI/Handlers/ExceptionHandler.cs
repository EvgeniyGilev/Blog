using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Handlers
{
    /// <summary>
    /// обработчик исключений
    /// </summary>
    public class ExceptionHandler : Attribute, IExceptionFilter
    {
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
