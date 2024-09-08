using BudgetTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.ViewModels
{
    public class ExpenseViewModel
    {
        public ExpenseViewModel()
        {
            Description = "Unknown";
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public float Cost { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Budget))]
        public int BudgetId { get; set; }

        [Required]
        public Budget Budget { get; set; } = null!;
    }
}
