using ControleDeBar.Dominio.ModuloMesa;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Infraestrutura.Compartilhado;
public class ControleDeBarDbContext : DbContext
{
    public DbSet<Mesa> Mesas { get; set; }

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

        base.OnModelCreating(modelBuilder);
    }
}
