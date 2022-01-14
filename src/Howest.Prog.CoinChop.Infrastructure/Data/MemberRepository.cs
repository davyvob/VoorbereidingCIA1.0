using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Howest.Prog.CoinChop.Infrastructure.Data
{
    public class MemberRepository : IMemberRepository
    {
        private CoinChopDataContext _dataContext;

        public MemberRepository(CoinChopDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Member Get(long id)
        {
            return _dataContext.Members
                .Include(m => m.Contributions)
                .SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<Member> GetByGroup(long expenseGroupId)
        {
            return _dataContext.Members
                    .Include(m => m.Contributions)
                    .Where(m => m.Group.Id == expenseGroupId);
        }

        public bool Create(Member member)
        {
            _dataContext.Members.Add(member);
            int recordsAdded = _dataContext.SaveChanges();
            return recordsAdded > 0;
        }

        public bool Delete(Member member)
        {
            _dataContext.Members.Remove(member);
            int recordsRemoved = _dataContext.SaveChanges();
            return recordsRemoved > 0;
        }

        public bool Update(Member member)
        {
            _dataContext.Members.Update(member);
            int recordsUpdated = _dataContext.SaveChanges();
            return recordsUpdated > 0;
        }

    }
}
