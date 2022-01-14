using Howest.Prog.CoinChop.Core.Entities;
using System.Linq;

namespace Howest.Prog.CoinChop.Core.Interfaces
{
    public interface IMemberRepository
    {
        Member Get(long id);

        IQueryable<Member> GetByGroup(long expenseGroupId);

        bool Create(Member member);

        bool Update(Member member);

        bool Delete(Member member);
    }
}
