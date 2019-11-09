using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project100.Models;
using Project100.Models.Class;

namespace Project100.Controllers
{
    [Authorize]
    public class BusinessesController : Controller
    {
        private readonly BankContext _context;

        public BusinessesController(BankContext context)
        {
            _context = context;
        }

        // GET: Businesses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Business.ToListAsync());
        }



        //Test

   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> View(string id)

        {

            var business = await _context.Business.FirstOrDefaultAsync
          (m => m.CustomerId == id);

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
              

                return View(business);
            }


        }
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
                    Business business = new Business();
                    business = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == id);


                    var newBalance = (business.Balance + amount);
                    business.Balance = newBalance;
                    _context.Update(business);
                    await _context.SaveChangesAsync();


                    Transaction transaction = new Transaction();
                    transaction.accountNumber = id;
                    transaction.accountType = "Business";
                    transaction.amount = amount;
                    transaction.date = DateTime.Now;
                    transaction.type = "Deposit";
                    transaction.balance = business.Balance;



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
                ViewData["ErrorMessage"] = "There was a problem with your withdrawl please try again";
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
            Business business = new Business();
            business = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == id);
            if (business.Balance+5000 >= amount)
            {

                try
                {
                                     
                    var newBalance = (business.Balance - amount);
                    business.Balance = newBalance;

                    _context.Update(business);
                    await _context.SaveChangesAsync();

                    Transaction transaction = new Transaction();
                    transaction.accountNumber = id;
                    transaction.accountType = "Business";
                    transaction.amount = amount;
                    transaction.date = DateTime.Now;
                    transaction.type = "Withdraw";
                    transaction.balance = business.Balance;



                    _context.Update(transaction);
                    await _context.SaveChangesAsync();





                }
                catch
                {
                    ViewData["ErrorMessage"] ="There was a problem with your withdrawl please try again";
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

            if (amount > 0) {

                try
                {
                    Business business = new Business();
                    business = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == id);

                    if (type == "checking")
                    {
                        Checking tochecking = new Checking();
                        tochecking = await _context.Checking.FirstOrDefaultAsync(c => c.accountNumber == transId);

                        if (tochecking != null)
                        {
                            if (business.CustomerId != tochecking.CustomerId)
                            {
                                ViewData["ErrorMessage"] = "You can only transfer between your own accounts";
                                return View();
                            }
                            else
                            {
                                if (business.Balance+5000 >= amount)
                                {

                                    var newBalance = (business.Balance - amount);
                                    business.Balance = newBalance;

                                    _context.Update(business);
                                    await _context.SaveChangesAsync();

                                    Transaction transaction = new Transaction();
                                    transaction.accountNumber = id;
                                    transaction.accountType = "Business";
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
                                    tranSecond.accountType = "Checking";
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
                                    ViewData["ErrorMessage"] = $"Amount is not sufficent .";
                                    return View();
                                }
                            }
                        }



                        else
                        {
                            ViewData["ErrorMessage"] = $"Please enter a valid account to transfer into.";
                            return View();
                        }
                    }
                    else
                    {
                        Business tobusiness = new Business();
                        tobusiness = await _context.Business.FirstOrDefaultAsync(c => c.accountNumber == transId);



                        if (tobusiness != null)
                        {
                            if (business.CustomerId != tobusiness.CustomerId)
                            {
                                ViewData["ErrorMessage"] = "You can only transfer between your own accounts";
                                return View();
                            }
                            else
                            {
                                if (business.Balance+5000 >= amount)
                                {

                                    var newBalance = (business.Balance - amount);
                                    business.Balance = newBalance;

                                    _context.Update(business);
                                    await _context.SaveChangesAsync();

                                    Transaction transaction = new Transaction();
                                    transaction.accountNumber = id;
                                    transaction.accountType = "Business";
                                    transaction.amount = amount;
                                    transaction.date = DateTime.Now;
                                    transaction.type = "Transfer/Withdraw";
                                    transaction.balance = business.Balance;



                                    _context.Update(transaction);
                                    await _context.SaveChangesAsync();


                                    var tonewBalance = (tobusiness.Balance + amount);
                                    tobusiness.Balance = tonewBalance;

                                    _context.Update(tobusiness);
                                    await _context.SaveChangesAsync();

                                    Transaction tranSecond = new Transaction();
                                    tranSecond.accountNumber = transId;
                                    tranSecond.accountType = "business";
                                    tranSecond.amount = amount;
                                    tranSecond.date = DateTime.Now;
                                    tranSecond.type = "transfer in";
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
                return RedirectToAction(nameof(View));


            }
            else
            {
                ViewData["ErrorMessage"] = "You can't transfer Negative Amount. ";
                return View();
            }

        }

        //End test



        // GET: Businesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Business
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (business == null)
            {
                return NotFound();
            }

            return View(business);
        }

        // GET: Businesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Business business)
        {
            if (ModelState.IsValid)
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                business.CustomerId = userId;
                business.createdAt = DateTime.Now;
                business.InterestRate = 5;
                business.type = "Business";

                _context.Add(business);
                await _context.SaveChangesAsync();
                


                Transaction transaction = new Transaction();
                transaction.accountNumber = business.accountNumber;
                transaction.accountType = "Business";
                transaction.amount = business.Balance;
                transaction.date = DateTime.Now;
                transaction.type = "Account Opening";
                transaction.balance = business.Balance;

                _context.Update(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));



            }
            return View(business);
        }

        // GET: Businesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Business.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }
            return View(business);
        }

        // POST: Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("type,accountNumber,InterestRate,Balance,createdAt,CustomerId")] Business business)
        {
            if (id != business.accountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(business);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessExists(business.accountNumber))
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
            return View(business);
        }

        // GET: Businesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Business
                .FirstOrDefaultAsync(m => m.accountNumber == id);
            if (business.Balance == 0)
            {



                if (business == null)
                {
                    return NotFound();
                }

                return View(business);

            }
            else
            {
                ViewData["ErrorMessage"] = "Plese Clear the balance please";
                return RedirectToAction(nameof(Index));
            }
        }
        

        

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var business = await _context.Business.FindAsync(id);

            if (business.Balance == 0)
            {

                Transaction transaction = new Transaction();
                transaction.accountNumber = business.accountNumber;
                transaction.accountType = business.type;
                transaction.balance = 0;
                transaction.date = DateTime.Now;
                transaction.type = "Account Closed";

                _context.Business.Remove(business);
                await _context.SaveChangesAsync();

                _context.Update(transaction);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ErrorMessage"] = "Plese Clear the balance please";
                return RedirectToAction(nameof(Index));
            }


        }

        private bool BusinessExists(int id)
        {
            return _context.Business.Any(e => e.accountNumber == id);
        }
    }
}
