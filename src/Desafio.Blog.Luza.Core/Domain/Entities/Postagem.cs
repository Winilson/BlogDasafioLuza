using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Blog.Luza.Core.Domain.Entities
{
    public class Postagem
    {
        [Key]
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O título pode ter no máximo 200 caracteres.")]
        public string Titulo { get; private set; }

        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        public string Conteudo { get; private set; }

        [Required]
        public DateTime DataCriacao { get; private set; }

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public ApplicationUser Usuario { get; private set; }

        [MaxLength(500, ErrorMessage = "A URL da imagem pode ter no máximo 500 caracteres.")]
        public string? ImagemUrl { get; private set; }

        private Postagem() { }

        
        public Postagem(string titulo, string conteudo, ApplicationUser usuario, string? imagemUrl = null)
        {
            Id = Guid.NewGuid();
            ValidarCampos(titulo, conteudo, usuario, imagemUrl);
            Titulo = titulo;
            Conteudo = conteudo;
            Usuario = usuario;
            DataCriacao = DateTime.UtcNow;
            ImagemUrl = imagemUrl;
        }

        public Postagem(Guid id, string titulo, string conteudo, ApplicationUser usuario, DateTime dataCriacao, string? imagemUrl = null)
        {
            Id = id != Guid.Empty
                ? id
                : throw new ArgumentException("O ID da postagem não pode ser vazio.", nameof(id));
            ValidarCampos(titulo, conteudo, usuario, imagemUrl);
            Titulo = titulo;
            Conteudo = conteudo;
            Usuario = usuario;
            DataCriacao = dataCriacao;
            ImagemUrl = imagemUrl;
        }

        public void Editar(string novoTitulo, string novoConteudo, string? novaImagemUrl = null)
        {
            if (string.IsNullOrWhiteSpace(novoTitulo) && string.IsNullOrWhiteSpace(novoConteudo) && ImagemUrl == novaImagemUrl)
                throw new ArgumentException("Nenhuma alteração foi feita.");

            if (!string.IsNullOrWhiteSpace(novoTitulo))
                Titulo = novoTitulo;

            if (!string.IsNullOrWhiteSpace(novoConteudo))
                Conteudo = novoConteudo;

            if (!string.IsNullOrWhiteSpace(novaImagemUrl) || novaImagemUrl != ImagemUrl)
                ImagemUrl = novaImagemUrl;
        }

        private static void ValidarCampos(string titulo, string conteudo, ApplicationUser usuario, string? imagemUrl)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título não pode ser vazio ou nulo.", nameof(titulo));

            if (string.IsNullOrWhiteSpace(conteudo))
                throw new ArgumentException("O conteúdo não pode ser vazio ou nulo.", nameof(conteudo));

            if (usuario == null)
                throw new ArgumentException("O usuário não pode ser nulo.", nameof(usuario));

            if (imagemUrl != null && imagemUrl.Length > 500)
                throw new ArgumentException("A URL da imagem excede o limite de 500 caracteres.", nameof(imagemUrl));
        }
    }
}
