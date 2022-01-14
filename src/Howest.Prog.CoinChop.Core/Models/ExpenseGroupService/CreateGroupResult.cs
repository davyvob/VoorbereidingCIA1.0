using Howest.Prog.CoinChop.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Models.ExpenseGroupService
{
    public class CreateGroupResult : ResultBase
    {
        public long CreatedGroupId { get; set; }

        public string GeneratedToken { get; set; }
    }
}