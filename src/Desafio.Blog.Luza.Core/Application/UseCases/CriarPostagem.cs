using Desafio.Blog.Luza.Core.Domain.Entities;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Desafio.Blog.Luza.Core.Application.UseCases
{
    public class CriarPostagem
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CriarPostagem(IPostagemRepositorio postagemRepositorio, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _postagemRepositorio = postagemRepositorio;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PostagemDto> HandleAsync(CriarPostagemRequest request)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Usuário não autenticado.");
            }

            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado.", nameof(userId));
            }

            
            var postagem = new Postagem(request.Titulo, request.Conteudo, usuario, request.ImagemUrl);
            await _postagemRepositorio.AdicionarAsync(postagem);

            return new PostagemDto
            {
                Id = postagem.Id,
                Titulo = postagem.Titulo,
                Conteudo = postagem.Conteudo,
                UsuarioNome = usuario.UserName,
                DataCriacao = postagem.DataCriacao,
                ImagemUrl = postagem.ImagemUrl
            };
        }
    }
}
