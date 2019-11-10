using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project100.Models.Class;

namespace Project100.Models
{
    public class CustomerAccount
    {
        public Customers Customers { get; set; }
        
       // public List<IAccount> Accounts { get; set; }
        public List<Checking> Checkings { get; set; }
        public List<Business> Businesses { get; set; }
        public List<Term> Terms{ get; set; }
        public List<Loan> Loans { get; set; }



    }
}
