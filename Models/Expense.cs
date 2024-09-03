using BudgetTracker.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BudgetTracker.Models
{
    public class Expense
    {
        public Expense()
        {
            Description = "Unknown";
            DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public float Cost { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [ForeignKey(nameof(Budget))]
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }
    }
}
