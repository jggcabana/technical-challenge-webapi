using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.Data.DBModels
{
    public class Blacklist
    {
        public int Id { get; set; }

        public string BlacklistType { get; set; }

        public string BlacklistValue { get; set; }

        public bool IsActive { get; set; }
    }
}
