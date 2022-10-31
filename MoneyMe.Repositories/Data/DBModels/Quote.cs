using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.Data.DBModels
{
    public class Quote
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }

        public decimal Amount { get; set; }

        public int Term { get; set; }
    }
}
