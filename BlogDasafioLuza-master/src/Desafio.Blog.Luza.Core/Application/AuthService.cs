using Desafio.Blog.Luza.Core.Domain.Entities;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Shared.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Desafio.Blog.Luza.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<object> RegistrarUsuarioAsync(RegistrarUsuarioRequest request)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(request.Email);
            if (usuarioExistente != null)
            {
                return new { Message = "E-mail já está em uso." };
            }

            var usuario = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Nome = request.Nome
            };

            var resultado = await _userManager.CreateAsync(usuario, request.Senha);
            if (!resultado.Succeeded)
            {
                return new
                {
                    Message = "Erro ao registrar usuário.",
                    Errors = resultado.Errors.Select(e => e.Description)
                };
            }

            return new { Message = "Usuário registrado com sucesso.", UsuarioId = usuario.Id };
        }

        public async Task<object> LoginAsync(LoginRequest request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null)
            {
                return new { Message = "E-mail ou senha inválidos." };
            }

            var resultado = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, false);
            if (!resultado.Succeeded)
            {
                return new { Message = "E-mail ou senha inválidos." };
            }

            var token = _jwtService.GerarToken(usuario);
            return new { Token = token };
        }
    }
}
