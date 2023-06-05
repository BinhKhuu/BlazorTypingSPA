using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingSPA.Api.Domain.Models;

namespace TypingSPA.Api.Domain.Services
{
    public interface IQuoteService
    {
        public Task<QuoteModel> GetRandomQuote();
    }
}
