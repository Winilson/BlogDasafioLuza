using Desafio.Blog.Luza.Core.Domain.Entities;

namespace Desafio.Blog.Luza.Core.Domain.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
    }
}
