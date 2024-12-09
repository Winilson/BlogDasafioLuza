using Microsoft.AspNetCore.Identity;

namespace Desafio.Blog.Luza.Core.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
    }
}
