using Desafio.Blog.Luza.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Blog.Luza.Core.Domain.Interfaces
{
    public interface IBlogDbContext
    {
        DbSet<Postagem> Postagens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
