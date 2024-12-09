using Desafio.Blog.Luza.Core.Domain.Entities;

namespace Desafio.Blog.Luza.Core.Domain.Interfaces;

public interface IPostagemRepositorio
{
    /// <summary>
    /// Retorna todas as postagens existentes.
    /// </summary>
    Task<List<Postagem>> ObterTodasAsync();

    /// <summary>
    /// Retorna uma postagem pelo seu ID.
    /// </summary>
    Task<Postagem?> ObterPorIdAsync(Guid postagemId);

    /// <summary>
    /// Adiciona uma nova postagem.
    /// </summary>
    Task AdicionarAsync(Postagem postagem);

    /// <summary>
    /// Atualiza uma postagem existente.
    /// </summary>
    Task AtualizarAsync(Postagem postagem);

    /// <summary>
    /// Remove uma postagem existente.
    /// </summary>
    Task RemoverAsync(Postagem postagem);
}
