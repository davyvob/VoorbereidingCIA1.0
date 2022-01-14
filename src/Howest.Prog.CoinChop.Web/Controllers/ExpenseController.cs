using Howest.Prog.CoinChop.Core.Common;
using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Models.ExpenseService;
using Howest.Prog.CoinChop.Core.Services;
using Howest.Prog.CoinChop.Web.Extensions;
using Howest.Prog.CoinChop.Web.Models.ExpenseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Howest.Prog.CoinChop.Web.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ExpenseService _expenseService;
        private readonly MemberService _memberService;
        private readonly IExpenseGroupRepository _expenseGroupRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMailService _notificationService;

        public ExpenseController(
            ExpenseService expenseService,
            MemberService memberService,
            IExpenseGroupRepository expenseGroupRepository,
            IMemberRepository memberRepository,
            IMailService notificationService) 
        {
            _expenseService = expenseService;
            _memberService = memberService;
            _expenseGroupRepository = expenseGroupRepository;
            _memberRepository = memberRepository;
            _notificationService = notificationService;
        }

        public IActionResult Add([FromRoute] string groupToken)
        {
            var viewModel = new AddExpenseViewModel();
            viewModel.GroupToken = groupToken;
            viewModel.Contributors = PopulateContributors(groupToken);
            viewModel.AddExpenseInput = new AddExpenseInput
            {
                Amount = 0M
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(AddExpenseInput addExpenseInput, [FromRoute] string groupToken)
        {
            try
            {
                //execute command
                AddExpenseResult result = _expenseService.AddExpenseToGroup(addExpenseInput);

                //determine HTTP output
                if (result.IsSuccess)
                {
                    //return to dashboard
                    return RedirectToAction(nameof(ExpenseGroupController.Dashboard),
                                            "ExpenseGroup",
                                            new { groupToken });
                }
                else
                {
                    //add validation errors to ASP.Net model state
                    ModelState.MapValidationResults(result.ValidationResults, "AddExpenseInput.");

                    //redisplay form
                    var viewModel = new AddExpenseViewModel
                    {
                        AddExpenseInput = addExpenseInput,
                        GroupToken = groupToken,
                        Contributors = PopulateContributors(groupToken),
                    };
                    return View(viewModel);
                }
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Remove(long id, [FromRoute] string groupToken)
        {
            try
            {
                //execute command
                RemoveExpenseResult result = _expenseService.RemoveExpense(id);

                //determine HTTP output
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(ExpenseGroupController.Dashboard),
                                            "ExpenseGroup",
                                            new { groupToken });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
        }

        private IEnumerable<SelectListItem> PopulateContributors(string groupToken)
        {
            List<SelectListItem> membersList = null;
            var group = _expenseGroupRepository.GetByToken(groupToken);
            if (group != null)
            {
                membersList = _memberRepository.GetByGroup(group.Id).Select(
                    member => new SelectListItem { Value = member.Id.ToString(), Text = $"{member.Name} ({member.Email})" }    
                ).ToList();
                membersList.Insert(0, new SelectListItem { Value = "0", Text = $"==== pick a contributor ====" });
            }
            return membersList;
        }
    }
}
