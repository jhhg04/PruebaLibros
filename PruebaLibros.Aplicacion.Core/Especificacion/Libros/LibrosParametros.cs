namespace PruebaLibros.Aplicacion.Core.Especificacion.Libros;

public class LibrosParametros
{
    /* Columnas de especificacion*/
    public string? Titulo { get; set; }
    public int? Ano { get; set; }
    public string? Genero { get; set; }
    public string? NombreAutor { get; set; }
    public int? IdAutor { get; set; }
    /*---------------------------*/

    public string? Ordenar { get; set; }
    public int IndicePagina { get; set; } = 1;

    private const int MaxTamanoPagina = 50;

    private int _tamañoPagina = 3;
    public int TamanoPagina
    {
        get => _tamañoPagina;
        set => _tamañoPagina = (value > MaxTamanoPagina) ? MaxTamanoPagina : value;
    }

    public string? Busqueda { get; set; }
}
