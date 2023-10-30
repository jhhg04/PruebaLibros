using PruebaLibros.Aplicacion.Core.Error;
using System.Net;
using System.Text.Json;

namespace PruebaLibros.Aplicacion.Core.Mapper;

public class HttpResponseMap<T>
{
    private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };
    public HttpResponseMap(T? response, bool error, HttpResponseMessage httpResponseMessage, CodigoErrorException detalleError = null!)
    {
        Error = error;
        Response = response;
        HttpResponseMessage = httpResponseMessage;
        DetalleError = detalleError;
    }

    public bool Error { get; set; }
    public CodigoErrorException DetalleError { get; set; }

    public T? Response { get; set; }

    public HttpResponseMessage HttpResponseMessage { get; set; }

    public async Task<string?> GetErrorMessageAsync()
    {
        if (!Error)
        {
            return null;
        }

        if (DetalleError is not null)
            return $"Error: {DetalleError.mensaje} - Detalle: {DetalleError.detalles}";

        var codigoEstatus = HttpResponseMessage.StatusCode;
        if (codigoEstatus == HttpStatusCode.NotFound)
        {
            //return "Recurso no encontrado";
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
        else if (codigoEstatus == HttpStatusCode.BadRequest)
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
        else if (codigoEstatus == HttpStatusCode.Unauthorized)
        {
            return "Tienes que logearte para hacer esta operación";
        }
        else if (codigoEstatus == HttpStatusCode.Forbidden)
        {
            return "No tienes permisos para hacer esta operación";
        }
        else if (codigoEstatus == HttpStatusCode.InternalServerError)
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }

        return "Ha ocurrido un error inesperado";
    }
}
