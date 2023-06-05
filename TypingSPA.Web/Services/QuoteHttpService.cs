using System.Net.Http;
using System.Text.Json;
using TypingSPA.Api.Domain.Models;

namespace TypingSPA.Web.Services
{
    public class QuoteHttpService
    {
        private readonly HttpClient _HttpClient;
        private readonly JsonSerializerOptions _options;

        public QuoteHttpService(IHttpClientFactory httpClientFactory) {
            _HttpClient = httpClientFactory.CreateClient();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }

        public async Task<QuoteModel> GetRandomQuote()
        {
            var response = await _HttpClient.GetAsync("http://localhost:7267/api/api/quote/random");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return JsonSerializer.Deserialize<QuoteModel>(content, _options);
        }
    }
}
