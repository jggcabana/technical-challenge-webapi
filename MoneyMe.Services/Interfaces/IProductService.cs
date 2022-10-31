using MoneyMe.Repositories.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}
