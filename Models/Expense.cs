using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BudgetTracker.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cost { get; set; }

        [Required]
        public string Description { get; set; }

        private DateTime _DateCreated;

        public DateTime DateCreated {
            get { return _DateCreated; } 
            set {  _DateCreated = new DateTime(); }
        }

    }
}
