using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMe.Repositories.Enums;
using MoneyMe.Repositories.ViewModels;

namespace MoneyMe.Services.Validators
{
    public class QuoteValidator : IQuoteValidator
    {
        private readonly IQuoteRepository _quoteRepository;
        public QuoteValidator(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
        }

        public async Task<bool> CheckIfBlackListed(QuoteViewModel quote)
        {
            string message = "";
            var isEmailBlacklisted = await _quoteRepository.CheckQuoteIfBlacklisted(quote, nameof(BlacklistType.EmailDomain));
            if (isEmailBlacklisted)
                message += $"Domain '{quote.Email.Split('@')[1]}' is not allowed. ";
            var isMobileBlacklisted = await _quoteRepository.CheckQuoteIfBlacklisted(quote, nameof(BlacklistType.MobileNumber));
            if (isMobileBlacklisted)
                message += $"Mobile number '{quote.Mobile}' is not allowed. ";

            if (!string.IsNullOrEmpty(message))
                throw new Exception(message);

            return isEmailBlacklisted && isMobileBlacklisted;
        }

        public async Task<bool> CheckIfUserIs18Years(QuoteViewModel quote)
        {
            var today = DateTime.UtcNow.Date;
            var dob = quote.DateOfBirth;
            var age = today.Year - dob.Year;

            // subtract years from today and compare that date to actual dob
            // will be unequal if leap year
            if (dob.Date > today.AddYears(-age))
                age--;

            return age >= 18;
        }
    }
}
