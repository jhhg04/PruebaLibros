namespace PruebaLibros.Aplicacion.Core.Adaptadores;

public class Paginacion<T> where T : class
{
    public int Conteo { get; set; }
    public int IndicePagina { get; set; }
    public int TamañoPagina { get; set; }
    public IReadOnlyList<T>? Datos { get; set; }
    public int TotalPaginas { get; set; }
}

public class PaginacionObject<T> where T : class
{
    public int Conteo { get; set; }
    public int IndicePagina { get; set; }
    public int TamañoPagina { get; set; }
    public T? Dato { get; set; }
    public int TotalPaginas { get; set; }
}
