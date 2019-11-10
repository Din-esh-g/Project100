using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TermsController : Controller
    {
        private readonly BankContext _context;

        public TermsController(BankContext context)
        {
            _context = context;
        }

      //  GET: Terms
        public async Task<IActionResult> Index()
        {

            return View(await _context.Term.ToListAsync());
            }
        //    public IActionResult ViewTerm(int id)
        //{


        //    ViewData["Id"] = id;
        //    return View();


        //}

        public async Task<IActionResult> ViewTerms()
        {
            return View(await _context.Term.ToListAsync());
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
            Term term = new Term();
            term = await _context.Term.FirstOrDefaultAsync(c => c.accountNumber == id);
            if (term.Balance >= amount)
            {

                try
                {

                    var newBalance = (term.Balance - amount);
                    term.Balance = newBalance;

                    _context.Update(term);
                    await _context.SaveChangesAsync();

                    Transaction transaction = new Transaction();
                    transaction.accountNumber = id;
                    transaction.accountType = "Term Deposits";
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

                return RedirectToAction(nameof(Index));
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
                    Term terms = new Term();
                    terms = await _context.Term.FirstOrDefaultAsync(c => c.accountNumber == id);

                    if (type == "checking")
                    {
                        Checking tochecking = new Checking();
                        tochecking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == transId);

                        if (tochecking != null)
                        {
                            if (terms.CustomerId != tochecking.CustomerId)
                            {
                                ViewData["ErrorMessage"] = "You can only transfer between your own accounts";
                                return View();
                            }
                            else
                            {
                                if (terms.Balance >= amount)
                                {

                                    var newBalance = (terms.Balance - amount);
                                    terms.Balance = newBalance;

                                    _context.Update(terms);
                                    await _context.SaveChangesAsync();

                                    Transaction transaction = new Transaction();
                                    transaction.accountNumber = id;
                                    transaction.accountType = "Terms";
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
                    else if (type == "Business")
                    {
                        Business tobusiness = new Business();
                        tobusiness = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == transId);



                        if (tobusiness != null)
                        {
                            if (terms.CustomerId != tobusiness.CustomerId)
                            {
                                ViewData["ErrorMessage"] = "You can only transfer between your own accounts";
                                return View();
                            }
                            else
                            {
                                if (terms.Balance >= amount)
                                {

                                    var newBalance = (terms.Balance - amount);
                                    terms.Balance = newBalance;

                                    _context.Update(terms);
                                    await _context.SaveChangesAsync();

                                    Transaction transaction = new Transaction();
                                    transaction.accountNumber = id;
                                    transaction.accountType = "Terms";
                                    transaction.amount = amount;
                                    transaction.date = DateTime.Now;
                                    transaction.type = "Transfer/Withdraw";
                                    transaction.balance = terms.Balance;



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
                            ViewData["ErrorMessage"] = "Please enter a valid account to transfer into.";
                            return View();
                        }
                    }
                }
                catch
                {
                    ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
                    return View();
                }
                return RedirectToAction(nameof(Index));


            }
            else
            {
                ViewData["ErrorMessage"] = "You can't transfer Negative Amount. ";
                return View();
            }

        }







        // GET: Terms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _context.Term
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // GET: Terms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Terms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("period,type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Term term)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                term.CustomerId = userId;
                term.createdAt = DateTime.Now;
                term.InterestRate = 5;
                term.type = "Term";
                _context.Add(term);
                await _context.SaveChangesAsync();

                Transaction transaction = new Transaction();
                transaction.accountNumber = term.accountNumber;
                transaction.accountType = "Term";
                transaction.amount = term.Balance;
                transaction.date = DateTime.Now;
                transaction.type = "Account Open";

                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }

        // GET: Terms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _context.Term.FindAsync(id);
            if (term == null)
            {
                return NotFound();
            }
            return View(term);
        }

        // POST: Terms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("period,type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Term term)
        {
            if (id != term.accountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(term);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermExists(term.accountNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }

        // GET: Terms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _context.Term
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (term == null)
            {
                return NotFound();
            }
            if (term.period == 0)
            {


                return View(term);
            }
            else
            {
                ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
                return View();

            }
        }

        // POST: Terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var term = await _context.Term.FindAsync(id);
            if (term.period == 0)
            {
                _context.Term.Remove(term);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ErrorMessage"] = "Not Mature Yet.";
                return View();
            }
        }

        private bool TermExists(int id)
        {
            return _context.Term.Any(e => e.accountNumber == id);
        }
    }
}
