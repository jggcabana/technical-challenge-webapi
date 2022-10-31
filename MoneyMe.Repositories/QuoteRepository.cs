using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Repositories.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        public Task<Quote> SaveQuote(SaveQuoteRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Test()
        {
            return Task.FromResult<int>(68); ;
        }
    }
}
