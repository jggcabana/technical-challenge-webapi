using MoneyMe.Repositories.Data;
using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using MoneyMe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repository.GetProducts();
        }
    }
}
