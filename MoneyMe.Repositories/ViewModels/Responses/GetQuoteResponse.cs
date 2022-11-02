using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.ViewModels.Responses
{
    public class GetQuoteResponse
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal AmountRequired { get; set; }

        public int Term { get; set; }

        public bool IsApplied { get; set; } = false;

        
    }
}
