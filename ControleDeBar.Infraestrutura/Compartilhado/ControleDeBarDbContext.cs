using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControleDeBar.Infraestrutura.Compartilhado;
public class ControleDeBarDbContext : DbContext
{
    public DbSet<Mesa> Mesas { get; set; }
    public DbSet<Garcom> Garcons { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Conta> Contas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString("SqlServer")!;

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

        modelBuilder.Entity<Conta>(contaBuilder =>
        {
            contaBuilder.ToTable("TBConta");

            contaBuilder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            contaBuilder.Property(c => c.Titular)
            .IsRequired()
            .HasColumnType("varchar(2000)");

            contaBuilder.Property(c => c.Abertura)
            .IsRequired()
            .HasColumnType("datetime2");

            contaBuilder.Property(c => c.Fechamento)
            .IsRequired()
            .HasColumnType("datetime2");

            contaBuilder.Property(c => c.EstaAberta)
            .IsRequired()
            .HasColumnType("bit");

            contaBuilder.HasOne(c => c.Mesa)
            .WithMany()
            .HasForeignKey("Mesa_Id")
            .HasConstraintName("FK_TBConta_TBMesa")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            contaBuilder.HasOne(c => c.Garcom)
            .WithMany()
            .HasForeignKey("Garcom_Id")
            .HasConstraintName("FK_TBConta_TBGarcom")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            contaBuilder.HasMany(c => c.Pedidos)
            .WithOne()
            .HasForeignKey("Conta_Id")
            .HasConstraintName("FK_TBConta_TBPedido")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}
