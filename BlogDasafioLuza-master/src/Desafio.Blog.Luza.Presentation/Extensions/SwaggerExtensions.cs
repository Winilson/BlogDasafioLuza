namespace Desafio.Blog.Luza.Presentation.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Desafio Blog API",
                    Version = "v1",
                    Description = "API versão 1"
                });
            });

            services.AddEndpointsApiExplorer();
        }
    }
}
