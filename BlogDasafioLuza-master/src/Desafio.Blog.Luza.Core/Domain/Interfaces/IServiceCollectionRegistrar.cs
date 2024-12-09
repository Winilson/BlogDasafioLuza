using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Desafio.Blog.Luza.Core.Domain.Interfaces
{
    public interface IServiceCollectionRegistrar
    {
        void RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}
