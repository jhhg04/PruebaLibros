using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PruebaLibros.Aplicacion.DTO;

public record ErrorDTO
{
    public string detalles { get; init; }
    public int codigo { get; init; }
    public string mensaje { get; init; }
}
public record In_AutorDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [DisplayName("Nombre Completo")]
    public string NombreCompleto { get; set; }

    [Required(ErrorMessage = "La Fecha de nacimiento es obligatoria")]
    [DisplayName("Fecha Nacimiento")]
    public string FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La Ciudad es obligatoria")]
    public string Ciudad { get; set; }
    [Required(ErrorMessage = "El Correo es obligatorio")]
    public string Correo { get; set; }

    public In_AutorDTO()
    {

    }

    public In_AutorDTO(string nombreCompleto, string fechaNacimiento, string ciudad, string correo)
    {
        NombreCompleto = nombreCompleto;
        FechaNacimiento = fechaNacimiento;
        Ciudad = ciudad;
        Correo = correo;
    }
}


public record AutorDTO
{
    [DisplayName("Id Autor")]
    public int IdAutor { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [DisplayName("Nombre Completo")]
    public string NombreCompleto { get; set; }

    [Required(ErrorMessage = "La Fecha de nacimiento es obligatoria")]
    [DisplayName("Fecha Nacimiento")]
    public string FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La Ciudad es obligatoria")]
    public string Ciudad { get; set; }
    [Required(ErrorMessage = "El Correo es obligatorio")]
    public string Correo { get; set; }

    public AutorDTO()
    { }
    public AutorDTO(int idAutor, string nombreCompleto, string fechaNacimiento, string ciudad, string correo)
    {
        IdAutor = idAutor;
        NombreCompleto = nombreCompleto;
        FechaNacimiento = fechaNacimiento;
        Ciudad = ciudad;
        Correo = correo;
    }
}
public record In_LibroDTO
{
    [Required(ErrorMessage = "El titulo es obligatorio")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "El año es obligatorio")]
    public int Año { get; set; }

    [Required(ErrorMessage = "El genero es obligatorio")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "El numero de paginas es obligatorio")]
    [DisplayName("Numero de paginas")]
    public int NumPaginas { get; set; }


    [Required(ErrorMessage = "El autor es obligatorio")]
    [DisplayName("Id Autor")]
    public int IdAutor { get; set; }

    public In_LibroDTO()
    {

    }

    public In_LibroDTO(string titulo, int año, string genero, int numPaginas, int idAutor)
    {
        Titulo = titulo;
        Año = año;
        Genero = genero;
        NumPaginas = numPaginas;
        IdAutor = idAutor;
    }
}
public record LibroDTO
{
    [DisplayName("Id Libro")]
    public int IdLibro { get; set; }

    [Required(ErrorMessage = "El titulo es obligatorio")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "El año es obligatorio")]
    public int Año { get; set; }

    [Required(ErrorMessage = "El genero es obligatorio")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "El numero de paginas es obligatorio")]
    [DisplayName("Numero de paginas")]
    public int NumPaginas { get; set; }

    [Required(ErrorMessage = "El autor es obligatorio")]
    [DisplayName("Id Autor")]
    public int IdAutor { get; set; }

    public AutorDTO? Autor { get; set; }
}

