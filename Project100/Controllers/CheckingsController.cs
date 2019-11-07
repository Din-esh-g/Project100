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
    public class CheckingsController : Controller
    {
        private readonly BankContext _context;

        public CheckingsController(BankContext context)
        {
            _context = context;
        }

        // GET: Checkings
        public async Task<IActionResult> Index()
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

                Transaction transaction = new Transaction();
                transaction.accountNumber = checking.accountNumber;
                transaction.accountType = "Checking";
                transaction.amount = checking.Balance;
                transaction.date = DateTime.Now;
                transaction.type = "Account Open";

                _context.Add(checking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
            _context.Checking.Remove(checking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckingExists(int id)
        {
            return _context.Checking.Any(e => e.accountNumber == id);
        }
    }
}
