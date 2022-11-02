using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.ViewModels.Requests;
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
        Task<double> CalculateQuote();

        Task<Quote> SaveQuote(SaveQuoteRequest request);

        Task<GetQuoteResponse> GetQuote(int id);
    }
}
