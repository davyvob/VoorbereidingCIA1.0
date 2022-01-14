namespace Howest.Prog.CoinChop.Core.Entities
{
    public class Expense
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public long ContributorId { get; set; }
        public Member Contributor { get; set; }

    }
}
