using Microsoft.EntityFrameworkCore;
using Project100.Models.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project100.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> context)
           : base(context)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=RevDB;integrated security=True;MultipleActiveResultSets=True;");
            }
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Checking> Checking { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<Loan> Loan { get; set; }
        //public DbSet<IAccount> Accounts { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

    }
}
