using BudgetTracker.Data;
using BudgetTracker.Models;
using BudgetTracker.ViewModels;
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
        public ActionResult Index(int? budgetId)
        {
            ViewBag.budgetId = budgetId;
            ViewBag.Alert = "";

            if (TempData.Count() > 0 && TempData.Keys.Contains("alert"))
            {
                ViewBag.Alert = TempData["alert"].ToString();
            }

            var expenses = _context.Expenses
                .OrderByDescending(expenseModel => expenseModel.DateCreated)
                .Include(expenseModel => expenseModel.Budget)
                .Where(expense => budgetId != null ? expense.Budget.Id == budgetId : true);

            return View(expenses.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expenses/Create
        public ActionResult CreateOrEdit(int budgetId, int? id)
        {
            ViewBag.budgetId = budgetId;
            Budget existingBudget = _context.Budget.Find(budgetId);
            ViewBag.Budgets = new SelectList(_context.Budget.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text");

            if (id != null && id != 0)
            {
                ViewData["Title"] = "Update Expense";
                ViewData["SubmitTitle"] = "Update Expense";

                Expense? ExistingExpense = _context.Expenses.Find(id);

                if (ExistingExpense != null)
                {
                    ExpenseViewModel expense = new ExpenseViewModel()
                    {
                        Id = ExistingExpense.Id,
                        Description = ExistingExpense.Description,
                        Cost = ExistingExpense.Cost,
                        BudgetId = budgetId
                    };

                    return View(ExistingExpense);
                }
            }

            ViewData["Title"] = "Create New Expense";
            ViewData["SubmitTitle"] = "Add Expense";

            ExpenseViewModel? newExpense = new ExpenseViewModel();

            return View(newExpense);
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(ExpenseViewModel expenseVM)
        {
            ViewBag.budgetId = expenseVM.BudgetId;

            try
            {
                string entityEvent = "";

                expenseVM.Budget = _context.Budget.Find(expenseVM.BudgetId);

                if (expenseVM.Id == 0)
                {
                    Expense expense = new Expense();

                    entityEvent = "created";

                    UpdateExpenseWithExpenseViewModel(expenseVM, expense);


                    _context.Expenses.Add(expense);
                } else
                {
                    Expense? expense = _context.Expenses.Find(expenseVM.Id);

                    entityEvent = "updated";

                    UpdateExpenseWithExpenseViewModel(expenseVM, expense);

                    _context.Expenses.Update(expense);
                }

                _context.SaveChanges();

                TempData["alert"] = $"Expense has been {entityEvent}.";

                return RedirectToAction("Index", new { budgetId = expenseVM.BudgetId });
            }
            catch
            {
                ViewBag.Alert = "Expense could not be created successfully.";
                return View();
            }
        }

        private void UpdateExpenseWithExpenseViewModel(ExpenseViewModel expenseVM, Expense? expense)
        {
            DateTime currentDate = DateTime.Now;

            expense.Cost = expenseVM.Cost;
            expense.Description = expenseVM.Description;
            expense.Budget = _context.Budget.Find(expenseVM.BudgetId);
            expense.BudgetId = expenseVM.BudgetId;

            if (expenseVM.Id == 0)
            {
                expense.DateCreated = currentDate;
            }
            
            expense.DateUpdated = currentDate;
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
