using System.Collections.Generic;

namespace Howest.Prog.CoinChop.Core.Entities
{
    public class Member
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long GroupId { get; set; }

        public ExpenseGroup Group { get; set; }

        public ICollection<Expense> Contributions { get; set; }

        public override string ToString()
        {
            return $"Member: {Name}";
        }
    }
}
