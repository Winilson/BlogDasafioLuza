using Desafio.Blog.Luza.Presentation.Middlewares;

namespace Desafio.Blog.Luza.Presentation.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UsePresentationPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<CustomResponseMiddleware>();
            app.MapControllers();
        }
    }
}
