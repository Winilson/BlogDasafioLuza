using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Blog.Luza.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="request">Dados do usuário a ser registrado.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        /// <response code="201">Usuário registrado com sucesso.</response>
        /// <response code="400">Dados inválidos na requisição.</response>
        /// <response code="409">E-mail já está em uso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioRequest request)
        {
            return await ResponseAsync(() => _authService.RegistrarUsuarioAsync(request));
        }

        /// <summary>
        /// Realiza o login de um usuário existente.
        /// </summary>
        /// <param name="request">Dados para autenticação (e-mail e senha).</param>
        /// <returns>Token JWT para autenticação.</returns>
        /// <response code="200">Login realizado com sucesso, retorna o token JWT.</response>
        /// <response code="400">Dados inválidos na requisição.</response>
        /// <response code="401">Credenciais inválidas.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return await ResponseAsync(() => _authService.LoginAsync(request));
        }
    }
}
