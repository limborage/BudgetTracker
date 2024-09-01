using BudgetTracker.Data;
using BudgetTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            var Expenses = _context.Expenses.ToList();

            return View(Expenses);
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expense expense)
        {
            try
            {
                //expense.DateCreated = DateTime.Now;
                _context.Expenses.Add(expense);
                _context.SaveChanges();

                TempData["alert"] = "Expense has been created.";

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Alert = "Expense could not be created successfully.";
                return View();
            }
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int id)
        {
            Expense? ExistingExpense = _context.Expenses.Find(id);

            return View(ExistingExpense);
        }

        // POST: Expenses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Expense expense)
        {
            try
            {
                _context.Expenses.Update(expense);
                _context.SaveChanges();

                TempData["alert"] = "Expense has been updated.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Alert = "Expense does not exist.";
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

        // POST: Expenses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
