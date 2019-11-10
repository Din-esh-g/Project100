using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project100.Models.Class
{
    public interface IAccount
    {
        string type { get; set; }
        int accountNumber { get; set; }
        double InterestRate { get; set; }
        double Balance { get; set; }
    }

        //public class IAccount
        //{

        //   public string type { get; set; }
        //   public int accountNumber { get; set; }
        //   public double InterestRate { get; set; }
        //  public  double Balance { get; set; }


        //}
    }
