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

        Task<int> Test();
    }
}
