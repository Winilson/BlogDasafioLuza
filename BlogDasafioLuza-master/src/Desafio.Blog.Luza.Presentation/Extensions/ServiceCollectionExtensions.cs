using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Presentation.Middlewares;

namespace Desafio.Blog.Luza.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var registrars = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IServiceCollectionRegistrar).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceCollectionRegistrar>();

            foreach (var registrar in registrars)
            {
                registrar.RegisterServices(services, configuration);
            }

            var chaveJwt = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(chaveJwt))
            {
                throw new InvalidOperationException("não está configurado no appsettings.json.");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveJwt))
                    };
                });

            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidateModelFilter());
            });
        }
    }
}
