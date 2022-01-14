using Howest.Prog.CoinChop.Core.Common;
using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Models;
using Howest.Prog.CoinChop.Core.Models.ExpenseService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Services
{
    public class ExpenseService

    {
        private readonly IExpenseGroupRepository _expenseGroupRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMemberRepository _memberRepository;

        public ExpenseService(
            IExpenseGroupRepository expenseGroupRepository,
            IExpenseRepository expenseRepository,
            IMemberRepository memberRepository
        )
        {
            _expenseGroupRepository = expenseGroupRepository;
            _expenseRepository = expenseRepository;
            _memberRepository = memberRepository;
        }

        public AddExpenseResult AddExpenseToGroup(AddExpenseInput input)
        {
            //validate input
            var validator = new ModelValidator();
            var validationResults = new List<ValidationResult>();

            Member contributor = null;

            validator.TryValidateModel(input, validationResults);

            //check if valid ContributorId given
            contributor = _memberRepository.Get(input.ContributorId);
            if (contributor == null)
                validationResults.Add(new ValidationResult($"Invalid member", new[] { nameof(input.ContributorId) }));

            if (input.Amount <= 0)
                validationResults.Add(new ValidationResult($"Amount must be greater than 0", new[] { nameof(input.Amount) }));

            var result = new AddExpenseResult();
            result.ValidationResults = validationResults;

            if (validationResults.Count == 0)
            {
                //add expense
                var expenseToAdd = new Expense
                {
                    Amount = input.Amount,
                    Description = input.Description,
                    ContributorId = contributor.Id
                };
                result.IsSuccess = _expenseRepository.Create(expenseToAdd);

                //populate successful result
                result.CreatedExpenseId = expenseToAdd.Id;
                result.AddedToMemberId = contributor.Id;
            }
            return result;
        }

        public RemoveExpenseResult RemoveExpense(long expenseId)
        {
            var expense = _expenseRepository.Get(expenseId);
            if (expense == null)
            {
                throw new EntityNotFoundException($"Expense with id {expenseId} not found");
            }
            else
            {
                var result = new RemoveExpenseResult();

                result.IsSuccess = _expenseRepository.Delete(expense);

                //populate successful result
                result.RemovedExpense = expense;
                return result;
            }
        }
    }
}
