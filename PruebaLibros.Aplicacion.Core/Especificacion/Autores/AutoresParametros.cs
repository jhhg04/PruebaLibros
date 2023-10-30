namespace PruebaLibros.Aplicacion.Core.Especificacion.Autores;

public class AutoresParametros
{
    /* Columnas de especificacion*/
    public string? NombreCompleto { get; set; }
    public string? Ciudad { get; set; }
    public string? Correo { get; set; }
    public string? Busqueda { get; set; }
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
}
