using System.ComponentModel.DataAnnotations;

namespace PruebaLibros.Dominio.Principal.Entidades;

public class Libro
{
    [Key]
    public int IdLibroPk { get; set; }
    public string Titulo { get; set; }
    public int Año { get; set; }
    public string Genero { get; set; }
    public int NumPaginas { get; set; }
    public int IdAutorFk { get; set; }
    public virtual Autor Autor { get; set; }
}
