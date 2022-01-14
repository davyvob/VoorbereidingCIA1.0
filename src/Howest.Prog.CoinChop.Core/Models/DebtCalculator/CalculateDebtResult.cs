using Howest.Prog.CoinChop.Core.Entities;
using System.Collections.Generic;

namespace Howest.Prog.CoinChop.Core.Models.DebtCalculator
{
    public class CalculateDebtResult : ResultBase
    {
        public IEnumerable<Debt> Debts { get; set; }
    }
}