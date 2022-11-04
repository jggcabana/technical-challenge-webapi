using MoneyMe.Repositories.Data.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.ViewModels
{
    public class QuoteViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Mobile { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal AmountRequired { get; set; }

        public int Term { get; set; }

        public bool IsApplied { get; set; } = false;

        public Product Product { get; set; }

        public decimal Repayment { get; set; }

        public decimal EstablishmentFee { get; set; }

        public decimal InterestAmount { get; set; }

        public int PaymentPeriods { get; set; }


    }
}
