using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudgetTracker.Data;
using BudgetTracker.Models;
using BudgetTracker.ViewModels;

namespace BudgetTracker.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly BudgetTrackerContext _context;

        public BudgetsController(BudgetTrackerContext context)
        {
            _context = context;
        }

        // GET: Budgets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Budget.ToListAsync());
        }

        // GET: Budgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: Budgets/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create New Budget Group";

            return View(new Budget());
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BudgetStartDate,BudgetEndDate")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(budget);
        }

        // GET: Expenses/Create
        public ActionResult CreateOrEdit(int? id)
        {
            if (id != null)
            {
                ViewData["Title"] = "Update Budget";
                ViewData["SubmitTitle"] = "Update Budget";

                Budget? ExistingBudget = _context.Budget.Find(id);

                if (ExistingBudget != null)
                {
                    return View(ExistingBudget);
                }
            }

            ViewData["Title"] = "Create New Expense";
            ViewData["SubmitTitle"] = "Add Expense";

            return View(new Budget());
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(Budget budget)
        {
            try
            {
                string entityEvent = "";

                if (!ModelState.IsValid)
                {
                    return View(budget);
                }

                if (budget.Id == 0)
                {
                    Budget newBudget = new Budget();

                    entityEvent = "created";

                    //UpdateExpenseWithExpenseViewModel(expenseVM, expense);


                    _context.Budget.Add(newBudget);
                }
                else
                {
                    Budget? existingBudget = _context.Budget.Find(budget.Id);

                    entityEvent = "updated";

                    //UpdateExpenseWithExpenseViewModel(expenseVM, expense);

                    _context.Budget.Update(existingBudget);
                }

                _context.SaveChanges();

                TempData["alert"] = $"Expense has been {entityEvent}.";

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Alert = "Expense could not be created successfully.";
                return View();
            }
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BudgetStartDate,BudgetEndDate")] Budget budget)
        {
            if (id != budget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.Id))
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
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await _context.Budget.FindAsync(id);
            if (budget != null)
            {
                _context.Budget.Remove(budget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _context.Budget.Any(e => e.Id == id);
        }
    }
}
