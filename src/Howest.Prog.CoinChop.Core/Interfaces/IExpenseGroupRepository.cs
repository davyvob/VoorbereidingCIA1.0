using Howest.Prog.CoinChop.Core.Entities;
using System.Linq;

namespace Howest.Prog.CoinChop.Core.Interfaces
{
    public interface IExpenseGroupRepository
    {
        ExpenseGroup Get(long id);

        ExpenseGroup GetByToken(string groupToken);

        IQueryable<ExpenseGroup> GetAll();

        bool Create(ExpenseGroup group);

        bool Update(ExpenseGroup group);

        bool Delete(long id);
    }
}
