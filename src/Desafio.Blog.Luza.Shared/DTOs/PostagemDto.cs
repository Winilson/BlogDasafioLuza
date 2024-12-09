using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Blog.Luza.Shared.DTOs
{
    public class PostagemDto
    {
        [Required(ErrorMessage = "O ID é obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O título pode ter no máximo 200 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        public string Conteudo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string? UsuarioNome { get; set; } // Nome do usuário associado à postagem

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime DataCriacao { get; set; }

        [MaxLength(500, ErrorMessage = "A URL da imagem pode ter no máximo 500 caracteres.")]
        public string? ImagemUrl { get; set; } // URL opcional para a imagem associada
    }
}
