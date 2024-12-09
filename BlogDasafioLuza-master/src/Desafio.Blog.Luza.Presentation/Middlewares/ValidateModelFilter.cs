using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Desafio.Blog.Luza.Presentation.Middlewares
{
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value?.Errors != null && ms.Value.Errors.Any())
                    .ToDictionary(
                     kvp => kvp.Key,
                     kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>()
         );

                context.Result = new BadRequestObjectResult(new { Erros = errors });
            }
        }
    }
}
