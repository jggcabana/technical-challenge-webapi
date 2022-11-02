using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneyMe.Repositories.Data;
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
        private readonly MoneyMeContext _context;

        public QuoteRepository(MoneyMeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

        public async Task<Quote> GetQuote(int id)
        {
            try
            {
                return await _context.Quotes.Include(x => x.User)
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Quote> SaveQuote(SaveQuoteRequest request)
        {
            try
            {
                var existing = await _context.Quotes.SingleOrDefaultAsync(x =>
                    x.User.FirstName == request.FirstName
                    && x.User.LastName == request.LastName
                    && x.User.DateOfBirth.Date == request.DateOfBirth.Date);

                if (existing != null)
                    return existing;

                var newUser = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    MobileNumber = request.Mobile
                };

                var newQuote = new Quote
                {
                    User = newUser,
                    Amount = request.AmountRequired,
                    Term = request.Term
                };

                var insertResult = await _context.Quotes.AddAsync(newQuote);
                await _context.SaveChangesAsync();
                return insertResult.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> Test()
        {
            return Task.FromResult<int>(68); ;
        }
    }
}
