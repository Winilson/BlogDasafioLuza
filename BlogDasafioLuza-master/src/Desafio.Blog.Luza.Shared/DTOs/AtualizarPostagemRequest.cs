using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Blog.Luza.Shared.DTOs
{
    public class AtualizarPostagemRequest
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        public string Conteudo { get; set; }

        [MaxLength(500, ErrorMessage = "A URL da imagem pode ter no máximo 500 caracteres.")]
        public string? ImagemUrl { get; set; }
    }
}
