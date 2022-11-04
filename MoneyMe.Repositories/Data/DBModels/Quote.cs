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

        public int? UserId { get; set; }

        public int? ProductId { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }

        public decimal Amount { get; set; }

        public int Term { get; set; }

        public bool IsApplied { get; set; }

        public decimal Repayment { get; set; }

        public decimal EstablishmentFee { get; set; }

        public decimal InterestAmount { get; set;  }

        public int PaymentPeriods { get; set; }
    }
}
