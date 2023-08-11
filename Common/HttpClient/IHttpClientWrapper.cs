namespace Common.HttpClient
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken token);
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent requestBody, CancellationToken token);
        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent requestBody, CancellationToken token);
        Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken token);
    }
}
