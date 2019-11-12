using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project100.Models;
using Project100.Models.Class;

namespace Project100.Controllers
{
    [Authorize]
    public class CheckingsController : Controller
    {
        private readonly BankContext _context;

        public CheckingsController(BankContext context)
        {
            _context = context;
        }
        //
        //private readonly ILogger<CheckingsController> _logger;


        //public CheckingsController(ILogger<CheckingsController> logger)
        //{
        //    _logger = logger;
        //}

        //Test End
        // GET: Checkings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Checking.ToListAsync());
        }


        public async Task<IActionResult> CheckingView()
        {
            return View(await _context.Checking.ToListAsync());
        }


        // GET: Checkings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checking = await _context.Checking
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (checking == null)
            {
                return NotFound();
            }

            return View(checking);
        }


        //test

        public IActionResult Deposit(int id)
        {

            ViewData["Id"] = id;
            return View();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(int id, int amount)
        {
            if (amount > 0)
            {


                try
                {
                    Checking checking = new Checking();
                    checking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == id);


                    var newBalance = (checking.Balance + amount);
                    checking.Balance = newBalance;
                    _context.Update(checking);
                    await _context.SaveChangesAsync();


                    Transaction transaction = new Transaction();
                    transaction.accountNumber = id;
                    transaction.accountType = "Checking";
                    transaction.amount = amount;
                    transaction.date = DateTime.Now;
                    transaction.type = "Deposit";
                    transaction.balance = checking.Balance;



                    _context.Update(transaction);
                    await _context.SaveChangesAsync();

                }
                catch
                {
                    ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
                    return View();
                }
                return RedirectToAction(nameof(CheckingView));
            }
            else
            {
                ViewData["ErrorMessage"] = "No Negative amount...........";
                return View();
            }

        }

        public IActionResult Withdraw(int id)
        {

            ViewData["Id"] = id;
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id, int amount)
        {
            Checking checking = new Checking();
            checking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == id);
            if (checking.Balance >= amount)
            {

                try
                {

                    var newBalance = (checking.Balance - amount);
                    checking.Balance = newBalance;

                    _context.Update(checking);
                    await _context.SaveChangesAsync();

                    Transaction transaction = new Transaction();
                    transaction.accountNumber = id;
                    transaction.accountType = "Business";
                    transaction.amount = amount;
                    transaction.date = DateTime.Now;
                    transaction.type = "Withdraw";
                    transaction.balance = newBalance;



                    _context.Update(transaction);
                    await _context.SaveChangesAsync();





                }
                catch
                {
                    ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
                    return View();
                }

                return RedirectToAction(nameof(CheckingView));
            }
            else
            {
                ViewData["ErrorMessage"] = "Your Balance is not sufficent to do transactions.";
                return View();
            }


        }


        


        public IActionResult Transfer(int id)
        {

            ViewData["Id"] = id;
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(int id, int amount, int transId, string type)
        {

            if (amount > 0)
            {

                try
                {
                  Checking checking = new Checking();
                    checking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == id);

                    if (type == "checking")
                    {
                        Checking tochecking = new Checking();
                        tochecking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == transId);

                        if (tochecking != null)
                        {
                            if (checking.CustomerId != tochecking.CustomerId)
                            {
                                ViewData["ErrorMessage"] = $"You can only transfer between your own accounts";
                                return View();
                            }
                            else
                            {
                                if (checking.Balance >= amount)
                                {

                                    var newBalance = (checking.Balance - amount);
                                    checking.Balance = newBalance;

                                    _context.Update(checking);
                                    await _context.SaveChangesAsync();

                                    Transaction transaction = new Transaction();
                                    transaction.accountNumber = id;
                                    transaction.accountType = "Checking";
                                    transaction.amount = amount;
                                    transaction.date = DateTime.Now;
                                    transaction.type = "Transfer/withdraw";
                                    transaction.balance = newBalance;
                                    // transaction.balance = business.Balance;



                                    _context.Update(transaction);
                                    await _context.SaveChangesAsync();



                                    var tonewBalance = (tochecking.Balance + amount);
                                    tochecking.Balance = tonewBalance;

                                    _context.Update(tochecking);
                                    await _context.SaveChangesAsync();


                                    Transaction tranSecond = new Transaction();
                                    tranSecond.accountNumber = transId;
                                    tranSecond.accountType = "Business";
                                    tranSecond.amount = amount;
                                    tranSecond.date = DateTime.Now;
                                    tranSecond.type = "Transfer/Deposit ";
                                    // transaction.balance = tochecking.Balance;

                                    tranSecond.balance = tonewBalance;


                                    _context.Update(tranSecond);
                                    await _context.SaveChangesAsync();

                                }
                                else
                                {
                                    ViewData["ErrorMessage"] = "Amount is not sufficent .";
                                    return View();
                                }
                            }
                        }



                        else
                        {
                            ViewData["ErrorMessage"] = "Please enter a valid account to transfer into.";
                            return View();
                        }
                    }
                    else
                    {
                        Business tobusiness = new Business();
                        tobusiness = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == transId);



                        if (tobusiness != null)
                        {
                            if (checking.CustomerId != tobusiness.CustomerId)
                            {
                                ViewData["ErrorMessage"] = "You can only transfer between your own accounts";
                                return View();
                            }
                            else
                            {
                                if (checking.Balance >= amount)
                                {

                                    var newBalance = (checking.Balance - amount);
                                    checking.Balance = newBalance;

                                    _context.Update(checking);
                                    await _context.SaveChangesAsync();

                                    Transaction transaction = new Transaction();
                                    transaction.accountNumber = id;
                                    transaction.accountType = "Checking";
                                    transaction.amount = amount;
                                    transaction.date = DateTime.Now;
                                    transaction.type = "Transfer/Withdraw";
                                    transaction.balance = checking.Balance;



                                    _context.Update(transaction);
                                    await _context.SaveChangesAsync();


                                    var tonewBalance = (tobusiness.Balance + amount);
                                    tobusiness.Balance = tonewBalance;

                                    _context.Update(tobusiness);
                                    await _context.SaveChangesAsync();

                                    Transaction tranSecond = new Transaction();
                                    tranSecond.accountNumber = transId;
                                    tranSecond.accountType = "Business";
                                    tranSecond.amount = amount;
                                    tranSecond.date = DateTime.Now;
                                    tranSecond.type = "Transfer/Depsoit";
                                    tranSecond.balance = tonewBalance;



                                    _context.Update(tranSecond);
                                    await _context.SaveChangesAsync();

                                }

                                else
                                {
                                    ViewData["ErrorMessage"] = "The amount is not sufficent .";
                                    return View();
                                }
                            }


                        }
                        else
                        {
                            ViewData["ErrorMessage"] = "Account Number Does not Match .";
                            return View();
                        }
                    }

                    //   else if   Loans toLoans = new Loans();
                    //  toLoans = await _context.Loans.FirstOrDefaultAsync(c => c.accountNumber == transId);
                    //all the ativities and logice to  tested here,
                    //else Terms toTerms = new Terms();
                    //
                }
                catch
                {
                    ViewData["ErrorMessage"] = "There was a problem with your Transfer Transactions please try again";
                    return View();
                }
                return RedirectToAction(nameof(CheckingView));


            }
            else
            {
                ViewData["ErrorMessage"] = "You can't transfer Negative Amount. ";
                return View();
            }

        }

        //End test





        // GET: Checkings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Checkings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Checking checking)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                checking.CustomerId = userId;
                checking.createdAt = DateTime.Now;
                checking.InterestRate = 5;
                checking.type = "Checking";
                _context.Add(checking);
                await _context.SaveChangesAsync();


                Transaction transaction = new Transaction();
                transaction.accountNumber = checking.accountNumber;
                transaction.accountType = "Checking";
                transaction.amount = checking.Balance;
                transaction.date = DateTime.Now;
                transaction.type = "Account Open";
                transaction.balance = checking.Balance;

                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CheckingView));
            }
            return View(checking);
        }

        // GET: Checkings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checking = await _context.Checking.FindAsync(id);
            if (checking == null)
            {
                return NotFound();
            }
            return View(checking);
        }

        // POST: Checkings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Checking checking)
        {
            if (id != checking.accountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckingExists(checking.accountNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CheckingView));
            }
            return View(checking);
        }

        // GET: Checkings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checking = await _context.Checking
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (checking == null)
            {
                return NotFound();
            }

            return View(checking);
        }

        // POST: Checkings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checking = await _context.Checking.FindAsync(id);
            if (checking.Balance == 0)
            {
                _context.Checking.Remove(checking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CheckingView));
            }
            else
            {
                ViewData["ErrorMessage"] = "Please clear the balance. ";
                 return View(checking);
            }
        }

        private bool CheckingExists(int id)
        {
            return _context.Checking.Any(e => e.accountNumber == id);
        }
    }
}
