using Desafio.Blog.Luza.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Blog.Luza.Adapters.Infrastructure.Persistence
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Postagem> Postagens { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Postagem>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Titulo)
                      .IsRequired()
                      .HasMaxLength(200); 

                entity.Property(p => p.Conteudo)
                      .IsRequired(); 

                entity.Property(p => p.DataCriacao)
                      .IsRequired(); 

                entity.Property(p => p.ImagemUrl)
                      .HasMaxLength(500); 

                
                entity.HasOne(p => p.Usuario)
                      .WithMany()
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
