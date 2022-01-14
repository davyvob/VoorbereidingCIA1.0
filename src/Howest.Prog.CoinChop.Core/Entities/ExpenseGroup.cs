using System.Collections.Generic;

namespace Howest.Prog.CoinChop.Core.Entities
{
    public class ExpenseGroup
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public ICollection<Member> Members { get; set; }

    }
}
