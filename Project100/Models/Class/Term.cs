using Project100.Models.Class;
using System;
using System.ComponentModel.DataAnnotations;

namespace Project100.Models
{
    public class Term : IAccount
    {
        [Display(Name = "Number of Month")]
        public int period { get; set; }
        [Display(Name = "Types")]
        public string type { get; set; }
        [Key]
        [Required]
        [Display(Name = "Account Number")]
        public int accountNumber { get; set; }
        [Display(Name = "Interest Rate")]

        public double InterestRate { get; set; }
        [Display(Name = "Balance")]
        public double Balance { get; set; }

        [Display(Name = "Opening Date")]
        [DataType(DataType.Date)]
        public DateTime createdAt { get; set; }

        [Display(Name = "Customer Id")]
        public string CustomerId { get; set; }
        public virtual Customers Customers { get; set; }

        //[Required]
        //[ForeignKey("Customers")]
        //public Customers Customers { get; set; }





    }
}