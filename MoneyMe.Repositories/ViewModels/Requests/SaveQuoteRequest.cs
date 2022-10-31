using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Repositories.ViewModels.Requests
{
    public class SaveQuoteRequest
    {
        public decimal AmountRequired { get; set; }

        public int Term { get; set; }   

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Mobile { get; set; }
        
        public string Email { get; set; }

    }
}
