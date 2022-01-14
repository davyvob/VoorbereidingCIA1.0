namespace Howest.Prog.CoinChop.Core.Models.MemberService
{
    public class AddMemberResult : ResultBase
    {
        public long CreatedMemberId { get; set; }

        public long AddedToGroupId { get; set; }


    }
}