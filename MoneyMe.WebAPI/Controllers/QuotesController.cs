using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Services.Interfaces;

namespace MoneyMe.WebAPI.Controllers
{
    [Route("api/quotes")]
    [ApiController]
    public class QuotesController : ControllerBase
    {

        private readonly ILogger<QuotesController> _logger;
        private readonly IQuoteService _quoteService;
        public QuotesController(ILogger<QuotesController> logger, IQuoteService quoteService)
        {
            _logger = logger;
            _quoteService = quoteService;
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
    }
}
