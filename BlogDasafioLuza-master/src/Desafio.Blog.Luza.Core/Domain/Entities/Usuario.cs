using System.ComponentModel.DataAnnotations;

namespace Desafio.Blog.Luza.Core.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; init; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        [StringLength(200, ErrorMessage = "O e-mail deve ter no máximo 200 caracteres.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        public string Senha { get; init; }

        public Usuario(string nome, string email, string senha)
        {
            Id = Guid.NewGuid();
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Senha = senha ?? throw new ArgumentNullException(nameof(senha));
        }

        private Usuario()
        {
            Nome = string.Empty; 
            Email = string.Empty; 
            Senha = string.Empty; 
        }
    }
}
