using Howest.Prog.CoinChop.Core.Models.MemberService;

namespace Howest.Prog.CoinChop.Web.Models.MemberModels
{
    public class AddMemberViewModel
    {
        public string GroupToken { get; set; }

        public AddMemberInput AddMemberInput { get; set; }
    }
}