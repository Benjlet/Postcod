using Postcod.Implementation.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;

namespace Postcod.Implementation
{
    internal class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        internal HttpClientWrapper(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient(nameof(HttpClientWrapper));
        }

        public async Task<string> GetAsync(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response?.Content?.ReadAsStringAsync();
        }
    }
}