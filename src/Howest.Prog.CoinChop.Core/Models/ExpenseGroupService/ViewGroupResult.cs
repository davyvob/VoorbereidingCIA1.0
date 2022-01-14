using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Models;

namespace Howest.Prog.CoinChop.Core.Models.ExpenseGroupService
{
    public class ViewGroupResult : ResultBase
    {
        public ExpenseGroup Group { get; set; }
    }
}