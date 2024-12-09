using Desafio.Blog.Luza.Shared.DTOs;

namespace Desafio.Blog.Luza.Core.Domain.Interfaces
{
    public interface IPostService
    {
        /// <summary>
        /// Obtém todas as postagens.
        /// </summary>
        /// <returns>Lista de postagens.</returns>
        Task<IEnumerable<PostagemDto>> ObterTodasAsync();

        /// <summary>
        /// Obtém uma postagem por ID.
        /// </summary>
        /// <param name="id">ID da postagem.</param>
        /// <returns>Postagem correspondente ou null.</returns>
        Task<PostagemDto?> ObterPorIdAsync(Guid id);

        /// <summary>
        /// Cria uma nova postagem.
        /// </summary>
        /// <param name="request">Dados da nova postagem.</param>
        /// <returns>Dados da postagem criada.</returns>
        Task<PostagemDto> CriarAsync(CriarPostagemRequest request);

        /// <summary>
        /// Atualiza uma postagem existente.
        /// </summary>
        /// <param name="id">ID da postagem.</param>
        /// <param name="request">Dados para atualização da postagem.</param>
        /// <returns>True se a atualização foi bem-sucedida, False caso contrário.</returns>
        Task<bool> AtualizarAsync(Guid id, AtualizarPostagemRequest request);

        /// <summary>
        /// Remove uma postagem existente.
        /// </summary>
        /// <param name="id">ID da postagem.</param>
        /// <returns>True se a remoção foi bem-sucedida, False caso contrário.</returns>
        Task<bool> RemoverAsync(Guid id);
    }
}
