using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TypingSPA.Api.Domain.Services;
using TypingSPA.Api.Domain.Models;

namespace TypingSPA.Api.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly HttpClient _HttpClient;
        private readonly JsonSerializerOptions _options;

        public QuoteService(IHttpClientFactory httpClientFactory)
        {
            _HttpClient = httpClientFactory.CreateClient();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<QuoteModel> GetRandomQuote()
        {
            var response = await _HttpClient.GetAsync("https://api.quotable.io/random?minLength=100");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return  System.Text.Json.JsonSerializer.Deserialize<QuoteModel>(content, _options);
 
        }
    }
}
