using Moq;
using FluentAssertions;
using Desafio.Blog.Luza.Core.Domain.Entities;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Application.Services;
using Desafio.Blog.Luza.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Desafio.Blog.Luza.Tests.Services
{
    public class PostServiceTests
    {
        private readonly Mock<IPostagemRepositorio> _mockPostagemRepositorio;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            _mockPostagemRepositorio = new Mock<IPostagemRepositorio>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _postService = new PostService(
                _mockPostagemRepositorio.Object,
                _mockUserManager.Object,
                _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task ObterTodasAsync_DeveRetornarListaDePostagens()
        {
            // Arrange
            var usuario = new ApplicationUser { UserName = "TestUser" };
            var postagens = new List<Postagem>
            {
                new Postagem("Título 1", "Conteúdo 1", usuario),
                new Postagem("Título 2", "Conteúdo 2", usuario)
            };
            _mockPostagemRepositorio.Setup(repo => repo.ObterTodasAsync()).ReturnsAsync(postagens);

            // Act
            var result = await _postService.ObterTodasAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().ContainSingle(p => p.Titulo == "Título 1");
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarPostagem_SeExistir()
        {
            // Arrange
            var postagemId = Guid.NewGuid();
            var usuario = new ApplicationUser { UserName = "TestUser" };
            var postagem = new Postagem("Título", "Conteúdo", usuario);
            _mockPostagemRepositorio.Setup(repo => repo.ObterPorIdAsync(postagemId)).ReturnsAsync(postagem);

            // Act
            var result = await _postService.ObterPorIdAsync(postagemId);

            // Assert
            result.Should().NotBeNull();
            result!.Titulo.Should().Be("Título");
        }

        [Fact]
        public async Task CriarAsync_DeveRetornarPostagemCriada()
        {
            // Arrange
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "TestUser" };
            var request = new CriarPostagemRequest
            {
                Titulo = "Novo Título",
                Conteudo = "Novo Conteúdo",
                ImagemUrl = "http://example.com/image.jpg"
            };

            _mockHttpContextAccessor.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<string>()))
                .Returns(new Claim(ClaimTypes.NameIdentifier, usuario.Id));

            _mockUserManager.Setup(x => x.FindByIdAsync(usuario.Id)).ReturnsAsync(usuario);

            _mockPostagemRepositorio
                .Setup(repo => repo.AdicionarAsync(It.IsAny<Postagem>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _postService.CriarAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Titulo.Should().Be("Novo Título");
            result.UsuarioNome.Should().Be("TestUser");
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarTrue_SeAtualizacaoForBemSucedida()
        {
            // Arrange
            var postagemId = Guid.NewGuid();
            var usuario = new ApplicationUser { UserName = "TestUser" };
            var postagem = new Postagem("Título", "Conteúdo", usuario);
            var request = new AtualizarPostagemRequest
            {
                Titulo = "Título Atualizado",
                Conteudo = "Conteúdo Atualizado",
                ImagemUrl = "http://example.com/new-image.jpg"
            };

            _mockPostagemRepositorio.Setup(repo => repo.ObterPorIdAsync(postagemId)).ReturnsAsync(postagem);
            _mockPostagemRepositorio.Setup(repo => repo.AtualizarAsync(It.IsAny<Postagem>())).Returns(Task.CompletedTask);

            // Act
            var result = await _postService.AtualizarAsync(postagemId, request);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AtualizarAsync_DeveRetornarFalse_SePostagemNaoExistir()
        {
            // Arrange
            var postagemId = Guid.NewGuid();
            var request = new AtualizarPostagemRequest
            {
                Titulo = "Título Atualizado",
                Conteudo = "Conteúdo Atualizado",
                ImagemUrl = "http://example.com/new-image.jpg"
            };

            _mockPostagemRepositorio.Setup(repo => repo.ObterPorIdAsync(postagemId)).ReturnsAsync((Postagem?)null);

            // Act
            var result = await _postService.AtualizarAsync(postagemId, request);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarTrue_SeRemocaoForBemSucedida()
        {
            // Arrange
            var postagemId = Guid.NewGuid();
            var usuario = new ApplicationUser { UserName = "TestUser" };
            var postagem = new Postagem("Título", "Conteúdo", usuario);

            _mockPostagemRepositorio.Setup(repo => repo.ObterPorIdAsync(postagemId)).ReturnsAsync(postagem);
            _mockPostagemRepositorio.Setup(repo => repo.RemoverAsync(It.IsAny<Postagem>())).Returns(Task.CompletedTask);

            // Act
            var result = await _postService.RemoverAsync(postagemId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarFalse_SePostagemNaoExistir()
        {
            // Arrange
            var postagemId = Guid.NewGuid();
            _mockPostagemRepositorio.Setup(repo => repo.ObterPorIdAsync(postagemId)).ReturnsAsync((Postagem?)null);

            // Act
            var result = await _postService.RemoverAsync(postagemId);

            // Assert
            result.Should().BeFalse();
        }
    }
}
