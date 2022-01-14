using Howest.Prog.CoinChop.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Models.ExpenseGroupService
{
    public class ReplaceTokenResult : ResultBase
    {
        public long GroupId { get; set; }

        public string NewToken { get; set; }

    }
}