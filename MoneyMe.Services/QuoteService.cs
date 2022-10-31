using MoneyMe.Repositories.Interfaces;
using MoneyMe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        public QuoteService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }
        public async Task<double> CalculateQuote()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Test()
        {
            return await _quoteRepository.Test();
        }
    }
}
