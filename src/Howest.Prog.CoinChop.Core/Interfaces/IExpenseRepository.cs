using Howest.Prog.CoinChop.Core.Entities;
using System.Linq;

namespace Howest.Prog.CoinChop.Core.Interfaces
{
    public interface IExpenseRepository
    {
        Expense Get(long id);

        IQueryable<Expense> GetByGroup(long expenseGroupId);

        IQueryable<Expense> GetByContributor(long contributingMemberId);

        bool Create(Expense expense);

        bool Update(Expense expense);

        bool Delete(Expense expense);
    }
}
