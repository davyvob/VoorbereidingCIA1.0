using Howest.Prog.CoinChop.Core.Common;
using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Models.DebtCalculator;
using Howest.Prog.CoinChop.Core.Models.ExpenseGroupService;
using Howest.Prog.CoinChop.Core.Services;
using Howest.Prog.CoinChop.Web.Extensions;
using Howest.Prog.CoinChop.Web.Models;
using Howest.Prog.CoinChop.Web.Models.ExpenseGroupModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace Howest.Prog.CoinChop.Web.Controllers
{
    public class ExpenseGroupController : Controller
    {
        private readonly IMailService _notificationService;
        private readonly ExpenseGroupService _expenseGroupService;
        private readonly DebtCalculator _debtCalculator;

        public ExpenseGroupController(
            IMailService notificationService, 
            ExpenseGroupService expenseGroupService,
            DebtCalculator debtCalculator)
        {
            _notificationService = notificationService;
            _expenseGroupService = expenseGroupService;
            _debtCalculator = debtCalculator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AccessDashboard(AccessExpenseGroupInput accessInput)
        {
            if (accessInput.Token != null) 
            {
                return RedirectToAction(nameof(Dashboard), new { groupToken = accessInput.Token });
            }
            else 
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateGroupInput createInput)
        {
            //execute command
            CreateGroupResult result = _expenseGroupService.CreateGroup(createInput);

            //determine HTTP output
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Dashboard), new { groupToken = result.GeneratedToken });
            }
            else
            {
                //add validation errors to ASP.Net model state
                ModelState.MapValidationResults(result.ValidationResults);

                //redisplay form
                return View(createInput);
            }
        }

        [HttpGet]
        public IActionResult Dashboard([FromRoute] string groupToken)
        {
            try
            {
                var result = _expenseGroupService.ViewExpenseGroup(groupToken);

                if (result.IsSuccess)
                {
                    var viewModel = new DashboardViewModel
                    {
                        GroupId = result.Group.Id,
                        Name = result.Group.Name,
                        GroupToken = result.Group.Token,
                        Members = result.Group.Members,
                        SharedExpenses = result.Group.Members.SelectMany(e => e.Contributions).OrderBy(c => c.Id)
                    };

                    var calculateInput = new CalculateDebtInput { GroupId = result.Group.Id };
                    var calculatedResult = _debtCalculator.CalculateDebt(calculateInput);

                    if (calculatedResult.IsSuccess)
                    {
                        viewModel.Transactions = calculatedResult.Debts;
                        return View(viewModel);
                    }
                    else
                    {
                        throw new ApplicationException("Calculation failed");
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult ReplaceToken(ReplaceTokenInput replaceInput)
        {
            //execute command
            ReplaceTokenResult result = _expenseGroupService.ReplaceGroupToken(replaceInput);

            //determine HTTP output
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Dashboard), new { groupToken = result.NewToken });
            }
            else
            {
                //add validation errors to ASP.Net model state
                ModelState.MapValidationResults(result.ValidationResults, "ReplaceGroupTokenInput.");

                //redisplay form
                return View(replaceInput);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
