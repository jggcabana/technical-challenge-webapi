using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.Data.DBModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? InterestId { get; set; }

        public string Description { get; set; }

        public Interest Interest { get; set; }
    }
}
