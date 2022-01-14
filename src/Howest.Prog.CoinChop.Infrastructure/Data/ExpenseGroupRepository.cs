using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Howest.Prog.CoinChop.Infrastructure.Data
{
    public class ExpenseGroupRepository : IExpenseGroupRepository
    {
        private CoinChopDataContext _dataContext;

        public ExpenseGroupRepository(CoinChopDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ExpenseGroup Get(long id)
        {
            return _dataContext.ExpenseGroups
                .SingleOrDefault(e => e.Id == id);
        }
        public ExpenseGroup GetByToken(string groupToken)
        {
            return _dataContext.ExpenseGroups
                .Include(g => g.Members)
                .ThenInclude(m => m.Contributions)
                .SingleOrDefault(e => e.Token == groupToken);
        }

        public IQueryable<ExpenseGroup> GetAll()
        {
            return _dataContext.ExpenseGroups.AsQueryable();
        }

        public bool Create(ExpenseGroup group)
        {
            _dataContext.ExpenseGroups.Add(group);
            int recordsAdded = _dataContext.SaveChanges();
            return recordsAdded > 0;
        }

        public bool Delete(long id)
        {
            var expenseGroupToDelete = Get(id);
            _dataContext.ExpenseGroups.Remove(expenseGroupToDelete);
            int recordsRemoved = _dataContext.SaveChanges();
            return recordsRemoved > 0;
        }

        public bool Update(ExpenseGroup group)
        {
            _dataContext.ExpenseGroups.Update(group);
            int recordsUpdated = _dataContext.SaveChanges();
            return recordsUpdated > 0;
        }

    }
}
