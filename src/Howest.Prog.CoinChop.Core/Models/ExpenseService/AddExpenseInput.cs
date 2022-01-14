using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Models.ExpenseService
{
    public class AddExpenseInput
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Contributor")]
        public long ContributorId { get; set; }
    }
}