using Desafio.Blog.Luza.Shared.DTOs;

namespace Desafio.Blog.Luza.Core.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<object> RegistrarUsuarioAsync(RegistrarUsuarioRequest request);
        Task<object> LoginAsync(LoginRequest request);
    }
}
