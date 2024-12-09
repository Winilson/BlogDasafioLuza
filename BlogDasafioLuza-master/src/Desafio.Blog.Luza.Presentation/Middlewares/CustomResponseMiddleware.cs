using System.Net;
using System.Text.Json;

namespace Desafio.Blog.Luza.Presentation.Middlewares
{
    public class CustomResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomResponseMiddleware> _logger;

        public CustomResponseMiddleware(RequestDelegate next, ILogger<CustomResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Erro não tratado no middleware.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Details = exception.Message, 
                StatusCode = context.Response.StatusCode
            };

            var responseBody = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(responseBody);
        }
    }
}
