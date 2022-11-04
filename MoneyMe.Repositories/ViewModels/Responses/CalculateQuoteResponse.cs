using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.ViewModels.Responses
{
    public class CalculateQuoteResponse
    {
        public decimal Repayment { get; set; }

        public decimal EstablishmentFee { get; set; }

        public decimal PaymentPeriods { get; set; }

        public decimal TotalRepayments { get => (Math.Round(this.Repayment * 100) / 100) * this.PaymentPeriods; }
    }
}
