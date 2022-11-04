using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Repositories.ViewModels;
using MoneyMe.Repositories.ViewModels.Responses;
using MoneyMe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteValidator _quoteValidator;
        private readonly decimal ESTABLISHMENT_FEE;
        public QuoteService(IQuoteRepository quoteRepository, IQuoteValidator quoteValidator, IConfiguration config)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
            _quoteValidator = quoteValidator ?? throw new ArgumentNullException(nameof(quoteValidator));
            ESTABLISHMENT_FEE = Convert.ToDecimal(config.GetSection("AppSettings")["EstablishmentFee"]);
        }

        public async Task<QuoteViewModel> ApplyQuote(QuoteViewModel request)
        {
            // validate
            // TODO: refactor this

            var isBlackListed = await _quoteValidator.CheckIfBlackListed(request);
            if (isBlackListed)
                throw new Exception("User has email/mobile number that is blacklisted.");
            var isLegalAge = await _quoteValidator.CheckIfUserIs18Years(request);
            if (!isLegalAge)
                throw new Exception("User is less than 18 years of age and is ineligible for a loan.");

            // then save
            request.IsApplied = true;
            var result = await _quoteRepository.SaveQuote(request);
            if (result == null)
                throw new Exception("Something went wrong.");
            return request;
        }

        public async Task<CalculateQuoteResponse> CalculateQuote(QuoteViewModel request)
        {
            int excludeMonths = request.Product?.Interest?.StartFromNMonth - 1 ?? 0;

            int paymentPeriods = ((int)((request.Term / 12) * 52)) - excludeMonths;
            
            double rate = request.Product?.Interest != null ? (request.Product.Interest.Rate / 52) : 0;

            double nPer = (double)(paymentPeriods);
            double pv = (double)request.AmountRequired;

            var repayment = Math.Abs(Financial.Pmt(rate, nPer, pv));
            if (rate > 0)
            {
                request.InterestAmount = (decimal)((repayment * nPer) - pv) - ESTABLISHMENT_FEE;
            }
            else
            {
                request.InterestAmount = 0;
            }
            
            request.Repayment = (decimal)Math.Round(repayment * 100)/ 100;
            request.EstablishmentFee = (decimal)ESTABLISHMENT_FEE;    
            request.PaymentPeriods = paymentPeriods;

            var result = await _quoteRepository.SaveQuote(request);

            return new CalculateQuoteResponse
            {
                Repayment = (decimal)Math.Abs(repayment),
                PaymentPeriods = paymentPeriods,
                EstablishmentFee = ESTABLISHMENT_FEE
            };
        }

        public async Task<QuoteViewModel> GetQuote(int id)
        {
            var quote = await _quoteRepository.GetQuote(id);
            return new QuoteViewModel
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
                IsApplied = quote.IsApplied,
                Product = quote.Product,
                EstablishmentFee = quote.EstablishmentFee,
                Repayment = quote.Repayment,
                InterestAmount = quote.InterestAmount,
                PaymentPeriods = quote.PaymentPeriods
            };
        }

        public async Task<Quote> SaveQuote(QuoteViewModel request)
        {
            return await _quoteRepository.SaveQuote(request);
        }
    }
}
