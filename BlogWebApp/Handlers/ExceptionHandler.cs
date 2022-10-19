// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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