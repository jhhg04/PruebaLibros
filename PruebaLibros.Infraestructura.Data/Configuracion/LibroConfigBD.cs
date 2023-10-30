using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaLibros.Dominio.Principal.Entidades;

namespace PruebaLibros.Infraestructura.Data.Configuracion;

public class LibroConfigBD : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.Property(p => p.IdLibroPk).IsRequired().HasColumnType("int");
        builder.Property(p => p.Titulo).IsRequired().HasMaxLength(250).HasColumnType("varchar");
        builder.Property(p => p.Año).IsRequired().HasColumnType("int");
        builder.Property(p => p.Genero).IsRequired().HasMaxLength(250).HasColumnType("varchar");
        builder.Property(p => p.NumPaginas).IsRequired().HasColumnType("int");
        builder.HasOne(p => p.Autor).WithMany().HasForeignKey(p => p.IdAutorFk).OnDelete(DeleteBehavior.Restrict);
    }
}
