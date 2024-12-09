using Desafio.Blog.Luza.Core.Domain.Entities;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Desafio.Blog.Luza.Adapters.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Blog.Luza.Adapters.Infrastructure.Repositories;

public class PostagemRepositorio : IPostagemRepositorio
{
    private readonly BlogDbContext _context;

    public PostagemRepositorio(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Postagem>> ObterTodasAsync()
    {
        return await _context.Postagens
            .Include(p => p.Usuario) 
            .ToListAsync();
    }

    public async Task<Postagem?> ObterPorIdAsync(Guid postagemId)
    {
        return await _context.Postagens
            .Include(p => p.Usuario) 
            .FirstOrDefaultAsync(p => p.Id == postagemId);
    }

    public async Task AdicionarAsync(Postagem postagem)
    {
        await _context.Postagens.AddAsync(postagem);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Postagem postagem)
    {
        _context.Postagens.Update(postagem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Postagem postagem)
    {
        _context.Postagens.Remove(postagem);
        await _context.SaveChangesAsync();
    }
}
