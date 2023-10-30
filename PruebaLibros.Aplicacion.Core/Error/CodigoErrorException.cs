namespace PruebaLibros.Aplicacion.Core.Error;

public class CodigoErrorException : CodigoErrorResponse
{
    public CodigoErrorException(int codigoEstado, string mensaje = null, string detalle = null) : base(codigoEstado, mensaje)
    {
        detalles = detalle;
    }

    public CodigoErrorException(string detalle, int codigo, string mensaje) : base(codigo, mensaje)
    {
        detalles = detalle;
    }
    public string detalles { get; set; }
}