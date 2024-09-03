using BudgetTracker.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BudgetTracker.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Budget
    {
        public Budget()
        {
            var CurrentDate = DateTime.Now;
            var Month = CurrentDate.Day < 25 ? CurrentDate.Month : DateTime.Now.AddMonths(1).Month;

            Name = $"Budget - {CurrentDate.ToString("MMMM yyyy")}";
            BudgetEndDate = new DateTime(CurrentDate.Year, Month, 25);
            BudgetStartDate = BudgetEndDate.AddMonths(-1);
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BudgetStartDate { get; set; }

        [Required]
        public DateTime BudgetEndDate { get; set; }

        public ICollection<Expense> Expenses { get; } = new List<Expense>();
    }
}
