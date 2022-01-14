using Howest.Prog.CoinChop.Core.Entities;

namespace Howest.Prog.CoinChop.Core.Models.MemberService
{
    public class RemoveMemberResult : ResultBase
    {
        public Member RemovedMember { get; set; }
    }
}