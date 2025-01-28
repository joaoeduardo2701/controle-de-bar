﻿using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Infraestrutura.Compartilhado;
public class ControleDeBarDbContext : DbContext
{
    public DbSet<Mesa> Mesas { get; set; }
    public DbSet<Garcom> Garcons { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString =
           "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ControleDeBarOrm;Integrated Security=True";

        optionsBuilder.UseSqlServer(connectionString);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mesa>(mesaBuilder =>
        {
            mesaBuilder.ToTable("TBMesa");

            mesaBuilder.Property(d => d.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            mesaBuilder.Property(d => d.Numero)
            .IsRequired()
            .HasColumnType("varchar(100)");

            mesaBuilder.Property(d => d.Ocupada)
            .IsRequired()
            .HasColumnType("bit");
        });

        modelBuilder.Entity<Garcom>(garcomBuilder =>
        {
            garcomBuilder.ToTable("TBGarcom");

            garcomBuilder.Property(g => g.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            garcomBuilder.Property(g => g.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

            garcomBuilder.Property(g => g.CPF)
            .IsRequired()
            .HasColumnType("varchar(20)");
        });

        modelBuilder.Entity<Produto>(produtoBuilder =>
        {
            produtoBuilder.ToTable("TBProduto");

            produtoBuilder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            produtoBuilder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

            produtoBuilder.Property(p => p.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Pedido>(pedidoBuilder =>
        {
            pedidoBuilder.ToTable("TBPedido");

            pedidoBuilder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            pedidoBuilder.Property(p => p.QuantidadeSolicitada)
            .IsRequired()
            .HasColumnType("int");

            pedidoBuilder.HasOne(p => p.Produto)
            .WithMany()
            .HasForeignKey("Produto_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}
