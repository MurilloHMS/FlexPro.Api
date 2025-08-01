using FlexPro.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Veiculo> Veiculo { get; set; } = null!;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}