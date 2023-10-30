using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaLibros.Dominio.Principal.Entidades;

namespace PruebaLibros.Infraestructura.Data.Configuracion;

public class AutorConfigBD : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.Property(p => p.IdAutorPk).IsRequired().HasColumnType("int");
        builder.Property(p => p.NombreCompleto).IsRequired().HasMaxLength(300).HasColumnType("varchar");
        builder.Property(p => p.Ciudad).IsRequired().HasMaxLength(100).HasColumnType("varchar");
        builder.Property(p => p.Correo).IsRequired().HasMaxLength(150).HasColumnType("varchar");
        builder.Property(p => p.FechaNacimiento).IsRequired().HasColumnType("date");
    }
}
