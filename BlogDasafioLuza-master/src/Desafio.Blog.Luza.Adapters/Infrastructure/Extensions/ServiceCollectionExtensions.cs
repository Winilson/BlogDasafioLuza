using Desafio.Blog.Luza.Core.Domain.Entities;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Adapters.Infrastructure.Auth;
using Desafio.Blog.Luza.Adapters.Infrastructure.Persistence;
using Desafio.Blog.Luza.Adapters.Infrastructure.Repositories;
using Desafio.Blog.Luza.Adapters.Infrastructure.WebSockets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Blog.Luza.Adapters.Infrastructure.Extensions
{
    public class InfrastructureRegistrar : IServiceCollectionRegistrar
    {
        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<BlogDbContext>();
            services.AddScoped<IPostagemRepositorio, PostagemRepositorio>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<IWebSocketNotifier, WebSocketNotifier>();
        }
    }
}
