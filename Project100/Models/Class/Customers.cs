using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project100.Models.Class
{
    public class Customers
    {
        [Key]
        [Required]

        public int Id { get; set; }
        [Display(Name = "Main User Id")]
        public string registerId { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
    

     

      

        //public Business Business { get; set; }
        //public Checking Checking { get; set; }
        //public Term Term { get; set; }
        //public Loan Loan { get; set; }

        //public Transaction Transaction { get; set; }

        public List<Business> Business { get; set; }
        public List<Checking> Checking { get; set; }
        public List<Term> Terms { get; set; }
        public List<Loan> Loan { get; set; }
        public List<Transaction> Transaction { get; set; }



    }
}
