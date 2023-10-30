using PruebaLibros.Aplicacion.Core.Interfaces;
using PruebaLibros.Aplicacion.Core.Mapper;
using System.Text;
using System.Text.Json;

namespace PruebaLibros.Aplicacion.Core.Servicios;

public class ApiHttpService: IRestRepositorio
{
    private readonly HttpClient _httpClient;

    private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true, 
    };

    public ApiHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<T> DeserializaRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
    {
        var respuestaString = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(respuestaString, jsonSerializerOptions)!;
    }

    public async Task<HttpResponseMap<T>> GetAsync<T>(string url)
    {
        var responseHttp = await _httpClient.GetAsync(url);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await DeserializaRespuesta<T>(responseHttp, _jsonDefaultOptions);
            return new HttpResponseMap<T>(response, false, responseHttp);
        }

        return new HttpResponseMap<T>(default, true, responseHttp);
    }

    public async Task<HttpResponseMap<object>> PostAsync<T>(string url, T model)
    {
        var mesageJSON = JsonSerializer.Serialize(model);
        var messageContet = new StringContent(mesageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PostAsync(url, messageContet);
        return new HttpResponseMap<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpResponseMap<object>> PatchAsync<T>(string url, T model)
    {
        var mesageJSON = JsonSerializer.Serialize(model);
        var messageContet = new StringContent(mesageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PatchAsync(url, messageContet);
        return new HttpResponseMap<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpResponseMap<TResponse>> PostAsync<T, TResponse>(string url, T model)
    {
        var messageJSON = JsonSerializer.Serialize(model);
        var messageContet = new StringContent(messageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PostAsync(url, messageContet);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await DeserializaRespuesta<TResponse>(responseHttp, _jsonDefaultOptions);
            return new HttpResponseMap<TResponse>(response, false, responseHttp);
        }
        return new HttpResponseMap<TResponse>(default, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpResponseMap<object>> DeleteAsync(string url)
    {
        var responseHTTP = await _httpClient.DeleteAsync(url);
        return new HttpResponseMap<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
    }

    public async Task<HttpResponseMap<object>> PutAsync<T>(string url, T model)
    {
        var messageJSON = JsonSerializer.Serialize(model);
        var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PutAsync(url, messageContent);
        return new HttpResponseMap<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpResponseMap<TResponse>> PutAsync<T, TResponse>(string url, T model)
    {
        var messageJSON = JsonSerializer.Serialize(model);
        var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PutAsync(url, messageContent);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await DeserializaRespuesta<TResponse>(responseHttp, _jsonDefaultOptions);
            return new HttpResponseMap<TResponse>(response, false, responseHttp);
        }

        return new HttpResponseMap<TResponse>(default, !responseHttp.IsSuccessStatusCode, responseHttp);
    }
}
