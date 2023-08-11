namespace Common.HttpClient;
using BaseHttpClient = System.Net.Http.HttpClient;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly BaseHttpClient _httpClient;

    public HttpClientWrapper(BaseHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken token)
    {
        var response = await _httpClient.GetAsync(requestUri, token);

        return response;
    }

    public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent requestBody, CancellationToken token)
    {
        var response = await _httpClient.PostAsync(requestUri, requestBody, token);

        return response;
    }

    public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent requestBody, CancellationToken token)
    {
        var response = await _httpClient.PutAsync(requestUri, requestBody, CancellationToken.None);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken token)
    {
        var response = await _httpClient.DeleteAsync(requestUri, token);

        return response;
    }
}