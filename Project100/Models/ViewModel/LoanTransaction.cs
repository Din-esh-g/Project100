using Project100.Models.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project100.Models.ViewModel
{
    public class LoanTransaction
    {

        public Customers Customers { get; set; }
        public List<Loan> Loans { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
