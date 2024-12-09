using Desafio.Blog.Luza.Core.Domain.Entities;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Desafio.Blog.Luza.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostagemRepositorio _postagemRepositorio;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(
            IPostagemRepositorio postagemRepositorio,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _postagemRepositorio = postagemRepositorio;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<PostagemDto>> ObterTodasAsync()
        {
            var postagens = await _postagemRepositorio.ObterTodasAsync();
            return postagens.Select(p => new PostagemDto
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Conteudo = p.Conteudo,
                UsuarioNome = p.Usuario?.UserName,
                DataCriacao = p.DataCriacao,
                ImagemUrl = p.ImagemUrl
            });
        }

        public async Task<PostagemDto?> ObterPorIdAsync(Guid id)
        {
            var postagem = await _postagemRepositorio.ObterPorIdAsync(id);
            if (postagem == null) return null;

            return new PostagemDto
            {
                Id = postagem.Id,
                Titulo = postagem.Titulo,
                Conteudo = postagem.Conteudo,
                UsuarioNome = postagem.Usuario?.UserName,
                DataCriacao = postagem.DataCriacao,
                ImagemUrl = postagem.ImagemUrl
            };
        }

        public async Task<PostagemDto> CriarAsync(CriarPostagemRequest request)
        {
            var usuarioId = _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(usuarioId))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

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

        public async Task<bool> AtualizarAsync(Guid id, AtualizarPostagemRequest request)
        {
            var postagem = await _postagemRepositorio.ObterPorIdAsync(id);
            if (postagem == null) return false;

            postagem.Editar(request.Titulo, request.Conteudo, request.ImagemUrl);
            await _postagemRepositorio.AtualizarAsync(postagem);
            return true;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var postagem = await _postagemRepositorio.ObterPorIdAsync(id);
            if (postagem == null) return false;

            await _postagemRepositorio.RemoverAsync(postagem);
            return true;
        }
    }
}
