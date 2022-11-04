using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.ViewModels;
using MoneyMe.Repositories.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Services.Interfaces
{
    public interface IQuoteService
    {
        Task<CalculateQuoteResponse> CalculateQuote(QuoteViewModel request);

        Task<Quote> SaveQuote(QuoteViewModel request);

        Task<QuoteViewModel> ApplyQuote(QuoteViewModel request);

        Task<QuoteViewModel> GetQuote(int id);
    }
}
