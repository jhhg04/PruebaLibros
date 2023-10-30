using PruebaLibros.Aplicacion.Core.Mapper;

namespace PruebaLibros.Aplicacion.Core.Interfaces;

public interface IRestRepositorio
{
    Task<HttpResponseMap<T>> GetAsync<T>(string url);

    Task<HttpResponseMap<object>> PostAsync<T>(string url, T model);

    Task<HttpResponseMap<TResponse>> PostAsync<T, TResponse>(string url, T model);

    Task<HttpResponseMap<object>> DeleteAsync(string url);


    Task<HttpResponseMap<object>> PutAsync<T>(string url, T model);

    Task<HttpResponseMap<TResponse>> PutAsync<T, TResponse>(string url, T model);
}
