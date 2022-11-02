using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Services.Interfaces;

namespace MoneyMe.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    [EnableCors("AnyOrigin")]

    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _productService.GetProducts());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
