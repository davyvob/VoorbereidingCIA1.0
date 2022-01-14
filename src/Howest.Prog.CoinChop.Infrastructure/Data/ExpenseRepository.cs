using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Howest.Prog.CoinChop.Infrastructure.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private CoinChopDataContext _dataContext;

        public ExpenseRepository(CoinChopDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Expense Get(long id)
        {
            return _dataContext.Expenses
                .Include(m => m.Contributor)
                .SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<Expense> GetByContributor(long contributingMemberId)
        {
            return _dataContext.Expenses
                    .Include(m => m.Contributor)
                    .Where(e => e.ContributorId == contributingMemberId);
        }
        public IQueryable<Expense> GetByGroup(long expenseGroupId)
        {
            return _dataContext.Members
                    .Include(m => m.Contributions)
                    .Where(m => m.Group.Id == expenseGroupId)
                    .SelectMany(m => m.Contributions).Distinct();
        }

        public bool Create(Expense expense)
        {
            _dataContext.Expenses.Add(expense);
            int recordsAdded = _dataContext.SaveChanges();
            return recordsAdded > 0;
        }

        public bool Delete(Expense expense)
        {
            _dataContext.Expenses.Remove(expense);
            int recordsRemoved = _dataContext.SaveChanges();
            return recordsRemoved > 0;
        }

        public bool Update(Expense expense)
        {
            _dataContext.Expenses.Update(expense);
            int recordsUpdated = _dataContext.SaveChanges();
            return recordsUpdated > 0;
        }

    }
}
