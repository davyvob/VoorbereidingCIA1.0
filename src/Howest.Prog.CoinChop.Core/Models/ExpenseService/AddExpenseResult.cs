using Howest.Prog.CoinChop.Core.Models;

namespace Howest.Prog.CoinChop.Core.Models.ExpenseService
{
    public class AddExpenseResult : ResultBase
    {
        public long CreatedExpenseId { get; set; }

        public long AddedToMemberId { get; set; }

    }
}