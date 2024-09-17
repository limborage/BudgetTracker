using BudgetTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.ViewModels
{
    public class ExpenseViewIndexModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public float Cost { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public String BudgetName{ get; set; }
    }
}
