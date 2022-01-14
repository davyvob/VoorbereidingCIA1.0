using Howest.Prog.CoinChop.Core.Models.ExpenseService;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Howest.Prog.CoinChop.Web.Models.ExpenseModels
{
    public class AddExpenseViewModel
    {
        public string GroupToken { get; set; }

        public IEnumerable<SelectListItem> Contributors { get; set; }

        public AddExpenseInput AddExpenseInput { get; set; }
    }
}