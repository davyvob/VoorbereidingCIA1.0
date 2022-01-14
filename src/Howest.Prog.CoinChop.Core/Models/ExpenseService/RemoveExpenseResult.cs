using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Models;

namespace Howest.Prog.CoinChop.Core.Models.ExpenseService
{
    public class RemoveExpenseResult : ResultBase
    {
        public Expense RemovedExpense { get; set; }
    }
}