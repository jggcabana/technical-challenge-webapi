using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Repositories.ViewModels;
using MoneyMe.Services;
using MoneyMe.Services.Interfaces;
using MoneyMe.Services.Validators;
using Moq;

namespace MoneyMe.UnitTests
{
    [TestClass]
    public class QuoteTests
    {
        private IQuoteRepository _quoteRepositoryMock = Mock.Of<IQuoteRepository>();
        private IQuoteValidator _quoteValidatorMock = Mock.Of<IQuoteValidator>();
        private IConfiguration _configMock;

        private readonly QuoteService _quoteService;
        private readonly QuoteValidator _quoteValidator;
        private readonly QuoteViewModel _testQuote;
        private readonly Product _testProduct;
        private readonly Interest _testInterestStandard;
        private readonly Interest _testInterest2FreeMonths;

        public QuoteTests()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"AppSettings:EstablishmentFee", "300" }
            };

            _configMock = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _quoteValidator = new QuoteValidator(_quoteRepositoryMock);
            _quoteService = new QuoteService(_quoteRepositoryMock, _quoteValidatorMock, _configMock);

            _testInterestStandard = new Interest
            {
                Id = 1,
                Name = "Standard",
                Description = "",
                Rate = 0.1581,
                DurationMin = 0,
                DurationMax = -1,
                StartFromNMonth = 1
            };

            _testInterest2FreeMonths = new Interest
            {
                Id = 2,
                Name = "First 2 months free",
                Description = "",
                Rate = 0.1581,
                DurationMin = 6,
                DurationMax = -1,
                StartFromNMonth = 3
            };

            _testProduct = new Product
            {
                Id = 1,
                Name = "ProductA",
                Description = "Standard Loan",
                InterestId = 1,
                Interest = _testInterestStandard
            };

            _testQuote = new QuoteViewModel()
            {
                Id = 1,
                Title = "Ms.",
                FirstName = "Test",
                LastName = "Testing",
                DateOfBirth = DateTime.UtcNow,
                Mobile = "123456789",
                Email = "abc@123.com",
                AmountRequired = 5000,
                Term = 24,
                IsApplied = false,
                Product = _testProduct,
                Repayment = 0,
                EstablishmentFee = 300,
                PaymentPeriods = 0,
                InterestAmount = 0,
            };
        }

        [TestMethod]
        public async Task CheckIfBlackListed_ReturnsTrue_IfEmailIsBlacklisted()
        {
            try
            {
                // Arrange
                Mock.Get(_quoteRepositoryMock).Setup(x => x.CheckQuoteIfBlacklisted(_testQuote, "EmailDomain")).ReturnsAsync(true);

                // Act
                var result = await _quoteValidator.CheckIfBlackListed(_testQuote);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual($"Domain '{_testQuote.Email.Split('@')[1]}' is not allowed. ", e.Message);

            }
        }

        [TestMethod]
        public async Task CheckIfBlackListed_ReturnsFalse_IfEmailIsNotBlacklisted()
        {
            // Arrange
            Mock.Get(_quoteRepositoryMock).Setup(x => x.CheckQuoteIfBlacklisted(_testQuote, "EmailDomain")).ReturnsAsync(false);

            // Act
            var result = await _quoteValidator.CheckIfBlackListed(_testQuote);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public async Task CheckIfBlackListed_ReturnsTrue_IfMobilesBlacklisted()
        {
            try
            {
                // Arrange
                Mock.Get(_quoteRepositoryMock).Setup(x => x.CheckQuoteIfBlacklisted(_testQuote, "MobileNumber")).ReturnsAsync(true);

                // Act
                var result = await _quoteValidator.CheckIfBlackListed(_testQuote);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual($"Mobile number '{_testQuote.Mobile}' is not allowed. ", e.Message);
            }
        }

        [TestMethod]
        public async Task CheckIfBlackListed_ReturnsFalse_IfMobilesNotBlacklisted()
        {
            // Arrange
            Mock.Get(_quoteRepositoryMock).Setup(x => x.CheckQuoteIfBlacklisted(_testQuote, "MobileNumber")).ReturnsAsync(false);

            // Act
            var result = await _quoteValidator.CheckIfBlackListed(_testQuote);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public async Task CheckIfBlackListed_ReturnsFalse_IfBothBlacklisted()
        {
            try
            {
                // Arrange
                Mock.Get(_quoteRepositoryMock).Setup(x => x.CheckQuoteIfBlacklisted(_testQuote, "EmailDomain")).ReturnsAsync(true);
                Mock.Get(_quoteRepositoryMock).Setup(x => x.CheckQuoteIfBlacklisted(_testQuote, "MobileNumber")).ReturnsAsync(true);

                // Act
                var result = await _quoteValidator.CheckIfBlackListed(_testQuote);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual($"Domain '{_testQuote.Email.Split('@')[1]}' is not allowed. Mobile number '{_testQuote.Mobile}' is not allowed. ", e.Message);
            }
        }

        [TestMethod]
        public async Task CheckIfUserIs18Years_ReturnsFalse_OnLessThan18()
        {
            _testQuote.DateOfBirth = DateTime.UtcNow.AddYears(-18).AddDays(1).Date;
            var result = await _quoteValidator.CheckIfUserIs18Years(_testQuote);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CheckIfUserIs18Years_ReturnsFalse_OnThan18()
        {
            _testQuote.DateOfBirth = DateTime.UtcNow.AddYears(-18).Date;
            var result = await _quoteValidator.CheckIfUserIs18Years(_testQuote);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CheckIfUserIs18Years_ReturnsFalse_OnOlderThan18()
        {
            _testQuote.DateOfBirth = DateTime.UtcNow.AddYears(-18).AddDays(-1).Date;
            var result = await _quoteValidator.CheckIfUserIs18Years(_testQuote);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CalculateQuote_OnStandardInterest()
        {
            // Arrange
            var result = await _quoteService.CalculateQuote(_testQuote);

            Assert.AreEqual(result.TotalRepayments, (decimal)5839.60);
            Assert.AreEqual(Math.Round(result.Repayment * 100) / 100, (decimal)56.15);
            Assert.AreEqual(result.EstablishmentFee, 300);
        }

        [TestMethod]
        public async Task CalculateQuote_On2MonthFreeInterest()
        {
            // Arrange
            _testQuote.Product.Interest = _testInterest2FreeMonths;
            var result = await _quoteService.CalculateQuote(_testQuote);

            Assert.AreEqual(result.TotalRepayments, (decimal)5823.18);
            Assert.AreEqual(Math.Round(result.Repayment * 100) / 100, (decimal)57.09);
            Assert.AreEqual(result.EstablishmentFee, 300);
        }

        [TestMethod]
        public async Task CalculateQuote_FreeInterest()
        {
            // Arrange
            _testQuote.Product.Interest = null;
            var result = await _quoteService.CalculateQuote(_testQuote);

            Assert.AreEqual((int)result.TotalRepayments, (decimal)5000);
            Assert.AreEqual(Math.Round(result.Repayment * 100) / 100, (decimal)48.08);
            Assert.AreEqual(result.EstablishmentFee, 300);
        }
    }
}
