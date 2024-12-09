using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Blog.Luza.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Obtém todas as postagens disponíveis.
        /// </summary>
        /// <returns>Lista de postagens.</returns>
        /// <response code="200">Retorna a lista de postagens.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("obter/todos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostagemDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodas()
        {
            return await ResponseAsync(() => _postService.ObterTodasAsync());
        }

        /// <summary>
        /// Obtém uma postagem específica pelo ID.
        /// </summary>
        /// <param name="id">ID da postagem.</param>
        /// <returns>Postagem correspondente ao ID.</returns>
        /// <response code="200">Postagem encontrada.</response>
        /// <response code="404">Postagem não encontrada.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet("obter/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostagemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            return await ResponseAsync(() => _postService.ObterPorIdAsync(id));
        }

        /// <summary>
        /// Cria uma nova postagem.
        /// </summary>
        /// <param name="request">Dados para criação da postagem.</param>
        /// <returns>Detalhes da postagem criada.</returns>
        /// <response code="201">Postagem criada com sucesso.</response>
        /// <response code="400">Dados inválidos fornecidos na requisição.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [Authorize]
        [HttpPost("criar/post")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostagemDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Criar([FromBody] CriarPostagemRequest request)
        {
            return await ResponseAsync(() => _postService.CriarAsync(request));
        }

        /// <summary>
        /// Atualiza uma postagem existente.
        /// </summary>
        /// <param name="id">ID da postagem a ser atualizada.</param>
        /// <param name="request">Dados para atualização da postagem.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Postagem atualizada com sucesso.</response>
        /// <response code="400">Dados inválidos fornecidos na requisição.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Postagem não encontrada.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [Authorize]
        [HttpPut("atualiza/post{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarPostagemRequest request)
        {
            return await ResponseAsync(() => _postService.AtualizarAsync(id, request));
        }

        /// <summary>
        /// Remove uma postagem existente.
        /// </summary>
        /// <param name="id">ID da postagem a ser removida.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="204">Postagem removida com sucesso.</response>
        /// <response code="401">Usuário não autorizado.</response>
        /// <response code="404">Postagem não encontrada.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [Authorize]
        [HttpDelete("delete/post/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Remover(Guid id)
        {
            return await ResponseAsync(() => _postService.RemoverAsync(id));
        }
    }
}
