using FlexPro.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Veiculo> Veiculo { get; set; }= null!;
        public DbSet<Abastecimento> Abastecimento { get; set; } = null!;
        public DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public DbSet<Categoria> Categoria { get; set; } = null!;
        public DbSet<Entidade> Entidade { get; set; } = null!;
        public DbSet<Vendedor> Vendedor { get; set; } = null!;
        public DbSet<PrestadorDeServico> PrestadorDeServico { get; set; } = null!;
        public DbSet<Parceiro> Parceiro { get; set; } = null!;
        public DbSet<Receita> Receita { get; set; } = null!;
        public DbSet<Revisao> Revisao { get; set; } = null!;
        public DbSet<Produto> Produto { get; set; } = null!;
        public DbSet<ProdutoLoja> ProdutoLoja { get; set; } = null!;
        public DbSet<MateriaPrima> MateriaPrima { get; set; } = null!;
        public DbSet<Cliente> Cliente { get; set; } = null!;
        public DbSet<ApplicationUser> AspNetUsers { get; set; } = null!;
        public DbSet<Contato> Contato { get; set; } = null!;
        public DbSet<Embalagem> Embalagem { get; set; } = null!;
        public DbSet<Equipamento> Equipamento { get; set; } = null!;
        public DbSet<Computador> Computador { get; set; } = null!;
        public DbSet<AcessoRemoto> AcessoRemoto { get; set; } = null!;
        public DbSet<Especificacoes> Especificacoes { get; set; } = null!;
        public DbSet<InventoryMovement> InventoryMovement { get; set; } = null!;
        public DbSet<Products> Products { get; set; } = null!;


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
            
            modelBuilder.Entity<Revisao>()
                .HasOne(r => r.Local)
                .WithMany()
                .HasForeignKey(r => r.LocalId);

            modelBuilder.Entity<Revisao>()
                .HasOne(r => r.Veiculo)
                .WithMany()
                .HasForeignKey(r => r.VeiculoId);

            modelBuilder.Entity<Embalagem>()
                .HasOne(e => e.ProdutoLoja)
                .WithMany(p => p.Embalagems)
                .HasForeignKey(e => e.ProdutoLojaId);

            modelBuilder.Entity<Entidade>()
                .HasDiscriminator<string>("Tipo")
                .HasValue<Vendedor>("Vendedor")
                .HasValue<Parceiro>("Parceiro")
                .HasValue<Cliente>("Cliente")
                .HasValue<PrestadorDeServico>("PrestSer");

            modelBuilder.Entity<Produto>()
                .HasDiscriminator<string>("Tipo")
                .HasValue<ProdutoLoja>("ProdutoLoja")
                .HasValue<MateriaPrima>("materiaPrima");
            
            modelBuilder.Entity<ReceitaMateriaPrima>()
                .HasKey(rm => new { rm.ReceitaId, rm.MateriaPrimaId });

            modelBuilder.Entity<ReceitaMateriaPrima>()
                .HasOne(rm => rm.Receita)
                .WithMany(r => r.ReceitaMateriaPrima)
                .HasForeignKey(rm => rm.ReceitaId);
            
            modelBuilder.Entity<ReceitaMateriaPrima>()
                .HasOne(rm => rm.MateriaPrima)
                .WithMany(r => r.ReceitaMateriaPrima)
                .HasForeignKey(rm => rm.MateriaPrimaId);

            modelBuilder.Entity<Equipamento>()
                .HasDiscriminator<string>("Tipo")
                .HasValue<Computador>("Computador");

            modelBuilder.Entity<AcessoRemoto>()
                .HasOne(a => a.Computador)
                .WithMany(c => c.AcessosRemotos)
                .HasForeignKey(x => x.IdComputador);

            modelBuilder.Entity<Computador>()
                .HasOne(c => c.Especificacoes)
                .WithOne(e => e.Computador)
                .HasForeignKey<Especificacoes>(x => x.IdComputador);
        }
    }
}