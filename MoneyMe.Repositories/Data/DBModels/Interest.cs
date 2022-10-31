using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.Data.DBModels
{
    public class Interest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Rate { get; set; }

        public int DurationMin { get; set; }

        public int DurationMax { get; set; }

        public int StartFromNMonth { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
