using BudgetTracker.Data;
using BudgetTracker.Models;
using BudgetTracker.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly BudgetTrackerContext _context;

        public ExpensesController(BudgetTrackerContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public ActionResult Index()
        {
            ViewBag.Alert = "";

            if (TempData.Count() > 0 && TempData.Keys.Contains("alert"))
            {
                ViewBag.Alert = TempData["alert"].ToString();
            }

            var Expenses = _context.Expenses.OrderByDescending(expense => expense.DateCreated).Include(expense => expense.Budget).ToList();

            return View(Expenses);
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expenses/Create
        public ActionResult CreateOrEdit(int? id)
        {
            ViewBag.Budgets = new SelectList(_context.Budget.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text");

            if (id != null)
            {
                ViewData["Title"] = "Update Expense";
                ViewData["SubmitTitle"] = "Update Expense";

                Expense? ExistingExpense = _context.Expenses.Find(id);

                return View(ExistingExpense);
            }

            ViewData["Title"] = "Create New Expense";
            ViewData["SubmitTitle"] = "Add Expense";

            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(Expense expense)
        {
            try
            {
                string entityEvent = "";

                if (expense.Id == 0)
                {
                    entityEvent = "created";
                    expense.Budget = _context.Budget.FirstOrDefault(x => x.Id == expense.BudgetId) ?? null!;
                    _context.Expenses.Add(expense);
                } else
                {
                    entityEvent = "updated";
                    _context.Expenses.Update(expense);
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

        // GET: Expenses/Delete/5
        public ActionResult Delete(int id)
        {
            Expense? existingExpense = _context.Expenses.SingleOrDefault(model => model.Id == id);

            if (existingExpense != null)
            {
                _context.Expenses.Remove(existingExpense);
                _context.SaveChanges(true);
                TempData["alert"] = "Expense has been deleted.";
            } else
            {
                TempData["alert"] = "Expense does not exist.";
            }

            return RedirectToAction("Index");
        }
    }
}
