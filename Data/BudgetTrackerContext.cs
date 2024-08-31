using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using BudgetTracker.Models;

namespace BudgetTracker.Data
{
    public class BudgetTrackerContext : DbContext
    {
        public BudgetTrackerContext (DbContextOptions<BudgetTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}
