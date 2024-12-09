using Desafio.Blog.Luza.Core.Domain.Entities;

namespace Desafio.Blog.Luza.Core.Domain.Interfaces
{
    public interface IJwtService
    {
        string GerarToken(ApplicationUser usuario);
    }
}
