using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Repositories.ViewModels.Requests;
using MoneyMe.Repositories.ViewModels.Responses;
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

        public async Task<GetQuoteResponse> GetQuote(int id)
        {
            var quote = await _quoteRepository.GetQuote(id);
            return new GetQuoteResponse
            {
                Id = quote.Id,
                Title = quote.User.Title,
                FirstName = quote.User.FirstName,
                LastName = quote.User.LastName,
                DateOfBirth = quote.User.DateOfBirth,
                Mobile = quote.User.MobileNumber,
                Email = quote.User.Email,
                AmountRequired = quote.Amount,
                Term = quote.Term,
                IsApplied = quote.IsApplied
            };
        }

        public async Task<Quote> SaveQuote(SaveQuoteRequest request)
        {
            // validation 
            // check if email is in use
            // check if mobile is in use
            // check if email is not blacklisted
            // check if mobile is not blacklisted
            // check if date of birth is legal

            return await _quoteRepository.SaveQuote(request);
        }
    }
}
