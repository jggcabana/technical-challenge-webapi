using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoneyMe.Repositories.ViewModels.Requests;
using MoneyMe.Services.Interfaces;

namespace MoneyMe.WebAPI.Controllers
{
    [Route("api/quotes")]
    [ApiController]
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
                return Ok(await _quoteService.Test());
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveQuote(SaveQuoteRequest request)
        {
            try
            {
                var result = await _quoteService.SaveQuote(request);
                if (result == null)
                    throw new Exception("No data saved!");

                var createdUrl = $"{_baseUrl}/quotes/{result.Id}";

                return Created(createdUrl, result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
