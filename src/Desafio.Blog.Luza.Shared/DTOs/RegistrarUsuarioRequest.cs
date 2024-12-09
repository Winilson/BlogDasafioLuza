using System.ComponentModel.DataAnnotations;

namespace Desafio.Blog.Luza.Shared.DTOs
{
    public class RegistrarUsuarioRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        [MaxLength(200, ErrorMessage = "O e-mail deve ter no máximo 200 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        [MaxLength(50, ErrorMessage = "A senha deve ter no máximo 50 caracteres.")]
        public string Senha { get; set; }
    }
}
