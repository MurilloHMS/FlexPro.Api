﻿using FlexPro.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Api.Infrastructure.Persistance
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Veiculo> Veiculo { get; set; }= default!;
        public DbSet<Abastecimento> Abastecimento { get; set; } = default!;
        public DbSet<Funcionario> Funcionarios { get; set; } = default!;
        public DbSet<Categoria> Categoria { get; set; } = default!;
        public DbSet<Entidade> Entidade { get; set; } = default!;
        public DbSet<Vendedor> Vendedor { get; set; } = default!;
        public DbSet<PrestadorDeServico> PrestadorDeServico { get; set; } = default!;
        public DbSet<Parceiro> Parceiro { get; set; } = default!;
        public DbSet<Receita> Receita { get; set; } = default!;
        public DbSet<Revisao> Revisao { get; set; } = default!;
        public DbSet<Produto> Produto { get; set; } = default!;
        public DbSet<ProdutoLoja> ProdutoLoja { get; set; } = default!;
        public DbSet<MateriaPrima> MateriaPrima { get; set; } = default!;
        public DbSet<Cliente> Cliente { get; set; } = default!;
        public DbSet<ApplicationUser> AspNetUsers { get; set; } = default!;
        public DbSet<Contato> Contato { get; set; } = default!;
        public DbSet<Embalagem> Embalagem { get; set; } = default!;
        public DbSet<Equipamento>  Equipamento { get; set; } = default!;
        public DbSet<Computador> Computador { get; set; } = default!;
        public DbSet<AcessoRemoto> AcessoRemoto { get; set; } = default!;
        public DbSet<Especificacoes>  Especificacoes { get; set; } = default!;


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
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