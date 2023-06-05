using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using TypingSPA.Api.Domain.Services;

namespace TypingSPA.Api.Controllers
{
    public class QuoteFunctions
    {
        private IQuoteService _QuoteService;

        public QuoteFunctions(IQuoteService QuoteService)
        {
            _QuoteService = QuoteService;
        }

        [FunctionName("RandomQuote")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "api/quote/random")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var quote = await _QuoteService.GetRandomQuote();

            return new OkObjectResult(quote);
        }
    }
}
