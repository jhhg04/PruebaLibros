using Microsoft.EntityFrameworkCore;
using PruebaLibros.Dominio.Principal.Entidades;
using System.ComponentModel;
using System.Reflection;

namespace PruebaLibros.Infraestructura.Data;

public class ContextoBD : DbContext
{
    public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
    { }

    public DbSet<Autor> Autor { get; set; }
    public DbSet<Libro> Libro { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}