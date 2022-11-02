using MoneyMe.Repositories.Data.DBModels;
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
        Task<Quote> SaveQuote(SaveQuoteRequest request);

        Task<int> Test();

        Task<Quote> GetQuote(int id);
    }
}
