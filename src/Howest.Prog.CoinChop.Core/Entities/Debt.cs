namespace Howest.Prog.CoinChop.Core.Entities
{
    public class Debt
    {
        public Member Debtor { get; set; }

        public Member Creditor { get; set; }

        public decimal Amount { get; set; }
    }
}
