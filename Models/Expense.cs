using BudgetTracker.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BudgetTracker.Models
{
    public class Expense
    {
        public void Expenses()
        {
            this.Description = "Unknown";
            this.DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:00}")]
        public float Cost { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
