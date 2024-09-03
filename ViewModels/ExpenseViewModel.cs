using BudgetTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.ViewModels
{
    public class ExpenseViewModel
    {
        public Expense? Expense { get; set; }
        public Budget? Budget { get; set; }
    }
}
