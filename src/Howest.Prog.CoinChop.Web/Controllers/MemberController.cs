using Howest.Prog.CoinChop.Core.Common;
using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Models.MemberService;
using Howest.Prog.CoinChop.Core.Services;
using Howest.Prog.CoinChop.Web.Extensions;
using Howest.Prog.CoinChop.Web.Models.MemberModels;
using Microsoft.AspNetCore.Mvc;

namespace Howest.Prog.CoinChop.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMailService _notificationService;
        private readonly IExpenseGroupRepository _expenseGroupRepository;
        private readonly MemberService _memberService;

        public MemberController(IMailService notificationService, 
            MemberService memberService,
            IExpenseGroupRepository expenseGroupRepository)
        {
            _notificationService = notificationService;
            _memberService = memberService;
            _expenseGroupRepository = expenseGroupRepository;
        }

        public IActionResult Add([FromRoute] string groupToken)
        {
            var group = _expenseGroupRepository.GetByToken(groupToken);
            if(group == null)
            {
                return BadRequest();
            }
            else
            {
                var viewModel = new AddMemberViewModel
                {
                    GroupToken = groupToken,
                    AddMemberInput = new AddMemberInput { GroupId = group.Id }
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Add(AddMemberInput addMemberInput, [FromRoute] string groupToken)
        {
            try
            {
                //execute command
                AddMemberResult result = _memberService.AddMemberToGroup(addMemberInput);

                //determine HTTP output
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(ExpenseGroupController.Dashboard),
                                            "ExpenseGroup",
                                            new { groupToken });
                }
                else
                {
                    //add validation errors to ASP.Net model state
                    ModelState.MapValidationResults(result.ValidationResults, "AddMemberInput.");

                    //redisplay form
                    var viewModel = new AddMemberViewModel
                    {
                        AddMemberInput = addMemberInput,
                        GroupToken = groupToken
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
                RemoveMemberResult result = _memberService.RemoveMemberFromGroup(id);

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
    }
}
