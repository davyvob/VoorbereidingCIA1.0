using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Models.DebtCalculator
{
    public class CalculateDebtInput
    {
        [Required]
        public long GroupId { get; set; }
    }
}