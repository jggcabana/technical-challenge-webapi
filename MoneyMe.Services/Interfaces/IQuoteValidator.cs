using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Services.Interfaces
{
    public interface IQuoteValidator
    {
        Task<bool> CheckIfBlackListed(QuoteViewModel quote);

        Task<bool> CheckIfUserIs18Years(QuoteViewModel quote);
    }
}
