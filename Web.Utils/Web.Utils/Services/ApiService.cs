using System.Net;
using System.Text.Json;
using System.Text;

namespace Web.Utils.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<(T Data, HttpStatusCode StatusCode)> GetAsync<T>(string endpoint)
        {
            string urlApi = string.Concat(_baseUrl, endpoint);
            var response = await _httpClient.GetAsync(urlApi);
            var statusCode = response.StatusCode;

            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return (data, statusCode);
        }

        public async Task<(List<T> Data, HttpStatusCode StatusCode)> GetAllAsync<T>(string endpoint)
        {
            string urlApi = string.Concat(_baseUrl, endpoint);
            var response = await _httpClient.GetAsync(urlApi);
            var statusCode = response.StatusCode;

            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<T>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return (data, statusCode);
        }

        public async Task<HttpStatusCode> CreateAsync<T>(string endpoint, T data)
        {
            string urlApi = string.Concat(_baseUrl, endpoint);
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(urlApi, content);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateAsync<T>(string endpoint, T data)
        {
            string urlApi = string.Concat(_baseUrl, endpoint);
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(urlApi, content);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteAsync(string endpoint)
        {
            string urlApi = string.Concat(_baseUrl, endpoint);
            var response = await _httpClient.DeleteAsync(urlApi);

            return response.StatusCode;
        }
    }
}
