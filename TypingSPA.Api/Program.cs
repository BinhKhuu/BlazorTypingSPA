using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingSPA.Api.Domain.Services;
using TypingSPA.Api.Services;

[assembly: FunctionsStartup(typeof(TypingSPA.Api.Program))]
namespace TypingSPA.Api
{
    public class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IQuoteService, QuoteService>();
        }
    }
}
