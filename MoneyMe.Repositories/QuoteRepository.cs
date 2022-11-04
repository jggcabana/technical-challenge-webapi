using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneyMe.Repositories.Data;
using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Repositories.ViewModels;
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

        public async Task<bool> CheckQuoteIfBlacklisted(QuoteViewModel quote, string blacklistType)
        {
            try
            {
                bool isBlacklisted = false;
                switch (blacklistType)
                {
                    case "EmailDomain":
                        var domain = quote.Email.Split('@')[1];
                        isBlacklisted = await _context.Blacklists.AnyAsync(x => x.BlacklistType == "EmailDomain" && domain == x.BlacklistValue);
                        if (isBlacklisted)
                            return true;
                        break;
                    case "MobileNumber":
                        isBlacklisted = await _context.Blacklists.AnyAsync(x => x.BlacklistType == "MobileNumber" && quote.Mobile == x.BlacklistValue);
                        if (isBlacklisted)
                            return true;
                        break;
                    default:
                        break;
                }
                return isBlacklisted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Blacklist>> GetBlacklist(string blacklistType = "")
        {
            throw new NotImplementedException();
        }

        public async Task<Quote> GetQuote(int id)
        {
            try
            {
                return await _context.Quotes
                    .Include(x => x.User)
                    .Include(x => x.Product)
                        .ThenInclude(x => x.Interest)
                    .SingleOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Quote> SaveQuote(QuoteViewModel request)
        {
            try
            {
                var existing = default(Quote);

                if (request.Id > 0)
                    existing = await _context.Quotes.Include(x => x.User)
                        .SingleOrDefaultAsync(x => x.Id == request.Id);
                else
                    existing = await _context.Quotes.Include(x => x.User)
                        .FirstOrDefaultAsync(x =>
                            x.User.FirstName == request.FirstName
                            && x.User.LastName == request.LastName
                            && x.User.DateOfBirth.Date == request.DateOfBirth.Date);

                // update if existing
                if (existing != null)
                {
                    var updateEntry = new Quote
                    {
                        Id = existing.Id,
                        UserId = existing.Id,
                        ProductId = request.Product?.Id,
                        Repayment = request.Repayment,
                        Term = request.Term,
                        Amount = request.AmountRequired,
                        InterestAmount = request.InterestAmount,
                        EstablishmentFee = request.EstablishmentFee,
                        PaymentPeriods = request.PaymentPeriods,
                        IsApplied = request.IsApplied
                    };

                    _context.Entry(existing.User).CurrentValues.SetValues(new User
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        DateOfBirth = request.DateOfBirth,
                        Id = existing.User.Id,
                        Title = request.Title,
                        Email = request.Email,
                        MobileNumber = request.Mobile
                    });

                    _context.Entry(existing).CurrentValues.SetValues(updateEntry);

                    var result = await _context.SaveChangesAsync();
                    return updateEntry;
                }

                var newUser = new User
                {
                    Title = request.Title,
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
                    Term = request.Term,
                    Repayment = request.Repayment,
                    InterestAmount = request.InterestAmount,
                    IsApplied = request.IsApplied,
                    EstablishmentFee = request.EstablishmentFee,
                    PaymentPeriods = request.PaymentPeriods
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
    }
}
