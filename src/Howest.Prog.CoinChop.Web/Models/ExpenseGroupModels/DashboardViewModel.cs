using Howest.Prog.CoinChop.Core.Entities;
using System.Collections.Generic;

namespace Howest.Prog.CoinChop.Web.Models.ExpenseGroupModels
{
    public class DashboardViewModel
    {
        public long GroupId { get; set; }

        public string Name { get; set; }

        public string GroupToken { get; set; }

        public IEnumerable<Member> Members { get; set; }

        public IEnumerable<Expense> SharedExpenses { get; set; }

        public IEnumerable<Debt> Transactions { get; set; }
    }
}
