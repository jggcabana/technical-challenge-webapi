using Microsoft.EntityFrameworkCore;
using MoneyMe.Repositories.Data;
using MoneyMe.Repositories.Data.DBModels;
using MoneyMe.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MoneyMeContext _context;
        public ProductRepository(MoneyMeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
