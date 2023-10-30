using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PruebaLibros.Dominio.Principal.Entidades;

public class Autor
{

    [Key]
    public int IdAutorPk { get; set; }
    public string NombreCompleto { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Ciudad { get; set; }
    public string Correo { get; set; }
}
