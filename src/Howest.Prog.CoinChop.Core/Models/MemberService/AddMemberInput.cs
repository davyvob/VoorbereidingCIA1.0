using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Models.MemberService
{
    public class AddMemberInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public long GroupId { get; set; }
    }
}