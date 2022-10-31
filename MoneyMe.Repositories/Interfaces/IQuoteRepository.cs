using MoneyMe.Repositories.Data;
using MoneyMe.Repositories.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.Interfaces
{
    public interface IQuoteRepository
    {
        public Task<Quote> SaveQuote(SaveQuoteRequest request);

        public Task<int> Test();
    }
}
