using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Enums;
using MoneyMe.Repositories.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.Interfaces
{
    public interface IQuoteRepository
    {
        Task<Quote> SaveQuote(QuoteViewModel request);

        Task<Quote> GetQuote(int id);

        Task<IEnumerable<Blacklist>> GetBlacklist(string blacklistType = "");

        Task<bool> CheckQuoteIfBlacklisted(QuoteViewModel quote, string blacklistType);
    }
}
