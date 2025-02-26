using FlexPro.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Veiculo> Veiculo { get; set; } = default;
        public DbSet<Abastecimento> Abastecimento { get; set; } = default!;
        public DbSet<Funcionario> Funcionarios { get; set; } = default!;
        public DbSet<Categoria> Categoria { get; set; } = default!;
        public DbSet<Entidade> Entidade { get; set; } = default!;
        public DbSet<Receita> Receita { get; set; } = default!;
        public DbSet<Revisao> Revisao { get; set; } = default!;
        public DbSet<Produto> Produto { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Revisao>()
                .HasOne(r => r.Local)
                .WithMany()
                .HasForeignKey(r => r.LocalId);

            modelBuilder.Entity<Revisao>()
                .HasOne(r => r.Veiculo)
                .WithMany()
                .HasForeignKey(r => r.VeiculoId);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Receita)
                .WithMany()
                .HasForeignKey(p => p.IdReceita);
        }
        public DbSet<FlexPro.Api.Models.Cliente> Cliente { get; set; } = default!;
    }
}