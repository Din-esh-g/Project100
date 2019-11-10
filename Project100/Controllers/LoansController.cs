using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project100.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Project100.Models.Class;

namespace Project100.Controllers
{
    [Authorize]
    public class LoansController : Controller
    {
        private readonly BankContext _context;

        public LoansController(BankContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Loan.ToListAsync());
        }


        public async Task<IActionResult> CustomLoan()
        {
            return View(await _context.Loan.ToListAsync());
        }

        //Test 
        //public async Task<IActionResult> CustomLon(string id)
        //{

        //    if (id == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var loan = await _context.Loan.FirstOrDefaultAsync
        //      (m => m.CustomerId == id);
        //    if (loan != null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return View(loan);

        //}

        public IActionResult Payment(int id)
        {

            ViewData["Id"] = id;
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(int id, int amount)
        {
            if (amount > 0)
            {


                try
                {
                    Loan loan = new Loan();
                    loan = await _context.Loan.FirstOrDefaultAsync(c => c.accountNumber == id);

                    if (loan.Balance == 0 - amount)
                    {
                        var newBalance = (loan.Balance + amount);
                        loan.Balance = newBalance;
                        _context.Update(loan);
                        await _context.SaveChangesAsync();


                        Transaction transaction = new Transaction();
                        transaction.accountNumber = id;
                        transaction.accountType = "Loan";
                        transaction.amount = amount;
                        transaction.date = DateTime.Now;
                        transaction.type = "Deposit";
                        transaction.balance = newBalance;



                        _context.Update(transaction);
                        await _context.SaveChangesAsync();

                    }
                }
                catch
                {
                    ViewData["ErrorMessage"] = "There was a problem with  your Transaction.";
                    return View();
                }
            }

            else
            {
                ViewData["ErrorMessage"] = "No Negative amount...........";
                return View();
            }

                return RedirectToAction(nameof(CustomLoan));
            }
        

        





        public IActionResult Transfer(int id)
        {


            ViewData["Id"] = id;
            return View();



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(int id, int amount, int tid, string type)
        {


            try
            {
                Loan loan = new Loan();
                loan = await _context.Loan.FirstOrDefaultAsync(c => c.accountNumber == id);

                var x = 0 - amount;


                if (x < loan.Balance)
                {
                    ViewData["ErrorMessage"] = "You can not pay more than the balance of your loan";
                    return View();
                }

                else
                {
                    if (type == "checking")
                    {
                        Checking checking = new Checking();
                        checking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == tid);

                        if (checking != null)
                        {
                            if (checking.CustomerId != loan.CustomerId)
                            {
                                ViewData["ErrorMessage"] = "You can only pay from your own accounts";
                                return View();
                            }
                            else if (checking.Balance < amount)
                            {

                                ViewData["ErrorMessage"] = "You do not have enough money in that account to make this payment";
                                return View();
                            }

                            else
                            {
                                var newBalance = (checking.Balance - amount);
                                checking.Balance = newBalance;

                                Transaction transaction = new Transaction();
                                transaction.accountNumber = id;
                                transaction.accountType = "checking";
                                transaction.amount = amount;
                                transaction.date = DateTime.Now;
                                transaction.type = "transfer out";

                                _context.Update(checking);
                                await _context.SaveChangesAsync();


                                _context.Update(transaction);
                                await _context.SaveChangesAsync();

                                var newLoanBalance = (loan.Balance + amount);
                                loan.Balance = newLoanBalance;

                                Transaction totransaction = new Transaction();
                                totransaction.accountNumber = id;
                                totransaction.accountType = "loan";
                                totransaction.amount = amount;
                                totransaction.date = DateTime.Now;
                                totransaction.type = "Installment";


                                _context.Update(loan);
                                await _context.SaveChangesAsync();

                                _context.Update(totransaction);
                                await _context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            ViewData["ErrorMessage"] = "You have selected an invalid account";
                            return View();
                        }

                    }
                    else
                    {
                        Business business = new Business();
                        business = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == tid);

                        if (business != null)
                        {
                            if (business.CustomerId != loan.CustomerId)
                            {
                                ViewData["ErrorMessage"] = $"You can only pay from your own accounts";
                                return View();
                            }
                            else
                            {
                                var newBalance = (business.Balance - amount);
                                business.Balance = newBalance;

                                Transaction transaction = new Transaction();
                                transaction.accountNumber = id;
                                transaction.accountType = "business";
                                transaction.amount = amount;
                                transaction.date = DateTime.Now;
                                transaction.type = "transer out";

                                _context.Update(business);
                                await _context.SaveChangesAsync();


                                _context.Update(transaction);
                                await _context.SaveChangesAsync();

                                var newLoanBalance = (loan.Balance + amount);
                                loan.Balance = newLoanBalance;

                                Transaction totransaction = new Transaction();
                                totransaction.accountNumber = id;
                                totransaction.accountType = "loan";
                                totransaction.amount = amount;
                                totransaction.date = DateTime.Now;
                                totransaction.type = "made payment";


                                _context.Update(loan);
                                await _context.SaveChangesAsync();

                                _context.Update(totransaction);
                                await _context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            ViewData["ErrorMessage"] = "You have selected an invalid account";
                            return View();
                        }
                    }

                }
            }
            catch
            {
                ViewData["ErrorMessage"] = "There was a problem with your payment please try again";
                return View();
            }
            return RedirectToAction(nameof(CustomLoan));


        }


        //End test




        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                loan.Balance = (0 - loan.Balance);
                loan.CustomerId = userId;
                loan.createdAt = DateTime.Now;
                loan.InterestRate = 7;
                loan.type = "Loan";

                Transaction transaction = new Transaction();
                transaction.accountNumber = loan.accountNumber;
                transaction.accountType = "Loan";
                transaction.amount = loan.Balance;
                transaction.date = DateTime.Now;
                transaction.type = "Account Open";

                _context.Add(transaction);
                await _context.SaveChangesAsync();

                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CustomLoan));
            }
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Loan loan)
        {
            if (id != loan.accountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.accountNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CustomLoan));
            }
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (loan == null)
            {
                return NotFound();
            }

            //if (loan.Balance == 0)
            //{

                return View(loan);
            
            //else
            //{
            //    ViewData["ErrorMessage"] = "Plese Clear your account first.";
            //    return RedirectToAction(nameof(CustomLoan));
            //}
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            if (loan.Balance == 0)
            {

                _context.Loan.Remove(loan);
                await _context.SaveChangesAsync();

                Transaction transaction = new Transaction();
                transaction.accountNumber = loan.accountNumber;
                transaction.accountType = "Loan";
                transaction.amount = loan.Balance;
                transaction.date = DateTime.Now;
                transaction.type = "Account Closed";

                _context.Add(transaction);
                await _context.SaveChangesAsync();

         
            return RedirectToAction(nameof(CustomLoan));

        }else
            {
                ViewData["ErrorMessage"] = "Plese Clear your account first.";
                return RedirectToAction(nameof(CustomLoan));
    }
}

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.accountNumber == id);
        }
    }
}
