using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoneyMe.Repositories.ViewModels;
using MoneyMe.Services.Interfaces;

namespace MoneyMe.WebAPI.Controllers
{
    [Route("api/quotes")]
    [ApiController]
    [EnableCors("AnyOrigin")]
    public class QuotesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<QuotesController> _logger;
        private readonly IQuoteService _quoteService;

        private readonly string _baseUrl = "";
        public QuotesController(ILogger<QuotesController> logger, IConfiguration config, IQuoteService quoteService)
        {
            _logger = logger;
            _quoteService = quoteService;
            _quoteService = quoteService;
            _config = config;

            _baseUrl = config.GetSection("AppSettings")["BaseUrl"];
        }

        [HttpGet]
        [Route("{quoteId}")]
        public async Task<IActionResult> GetQuote(int quoteId)
        {
            try
            {
                return Ok(await _quoteService.GetQuote(quoteId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("{quoteId}/calculate")]
        public async Task<IActionResult> CalculateQuote(int quoteId, [FromBody] QuoteViewModel request)
        {
            try
            {
                return Ok(await _quoteService.CalculateQuote(request));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveQuote(QuoteViewModel request)
        {
            try
            {
                var result = await _quoteService.SaveQuote(request);
                if (result == null)
                    throw new Exception("No data saved!");

                var getUrl = $"{_baseUrl}/api/quotes/{result.Id}";
                var createdUrl = $"{_baseUrl}/quote-calculator/{result.Id}";

                return Created(getUrl, new { siteLink = createdUrl, data = result });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("{quoteId}/apply")]
        public async Task<IActionResult> ApplyQuote(QuoteViewModel request)
        {
            try
            {
                var result = await _quoteService.ApplyQuote(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
