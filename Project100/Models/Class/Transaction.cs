using Project100.Models.Class;
using System;
using System.ComponentModel.DataAnnotations;

namespace Project100.Models
{
    public class Transaction
    {
        
            [Required]
            [Key]
            public int Id { get; set; }

            [Display(Name = "Account Number")]
            public int accountNumber { get; set; }
            [Display(Name = "Number of Month")]
            public int numberOfMonth { get; set; }
            [Display(Name = "Account Types")]
            public string accountType { get; set; }

            [Display(Name = " Amount")]
            public double amount { get; set; }

            [Display(Name = "Transaction Date")]
            [DataType(DataType.Date)]
            public DateTime date { get; set; }

            [Display(Name = "Transaction Types")]
            public string type { get; set; }
            [Display(Name = "Balance")]
            public double balance { get; set; }
        public string CustomerId { get; set; }
            public Customers Customer { get; set; }
            public Checking Checking { get; set; }
            public Business Business { get; set; }
            public Loan Loan { get; set; }
            public Term Term { get; set; }

            //public List<Business> Businesses { get; set; }
            //public List<Checking> Checkings { get; set; }
            //public List<Term> Terms { get; set; }
            //public List<Loan> Loans { get; set; }
            //public List<Customers> Customers{ get; set; }


        }
    }