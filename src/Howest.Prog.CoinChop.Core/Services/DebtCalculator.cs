using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Models;
using Howest.Prog.CoinChop.Core.Models.DebtCalculator;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Howest.Prog.CoinChop.Core.Services
{
    public class DebtCalculator
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseGroupRepository _expenseGroupRepository;

        public DebtCalculator(
           IExpenseRepository expenseRepository,
           IExpenseGroupRepository expenseGroupRepository)
        {
            _expenseGroupRepository = expenseGroupRepository;
            _expenseRepository = expenseRepository;
        }

        public CalculateDebtResult CalculateDebt(CalculateDebtInput input)
        {
            //validate input
            var validator = new ModelValidator();
            var validationResults = new List<ValidationResult>();
            validator.TryValidateModel(input, validationResults);

            //check if valid GroupId is given
            var group = _expenseGroupRepository.Get(input.GroupId);
            if (group == null)
                validationResults.Add(new ValidationResult($"Invalid group", new[] { nameof(input.GroupId) }));

            var result = new CalculateDebtResult();
            result.ValidationResults = validationResults;

            if (validationResults.Count == 0)
            {
                var expenses = _expenseRepository.GetByGroup(group.Id).ToList();

                var expensePerMember = expenses
                    .GroupBy(e => e.Contributor)
                    .ToDictionary(g => g.Key, g => g.Sum(expense => expense.Amount));

                //add zero-contributing members
                foreach(var member in group.Members)
                {
                    if (!expensePerMember.ContainsKey(member))
                        expensePerMember.Add(member, 0);
                }

                //holds transactions between members
                var transactions = new List<Debt>();

                if (expensePerMember.Keys.Count > 0)
                {
                    //this is the average amount spent. In the end, all member should have paid this.
                    decimal memberExpenseGoal = expenses.Sum(e => e.Amount) / expensePerMember.Keys.Count;

                    //divide group into givers (debtor) and receivers (creditors)
                    //and ignore members who spent exactly the goal (= no transaction needed)
                    //save the amount they must give or receive in a dictionary
                    var giverDebts = expensePerMember.Where(e => e.Value < memberExpenseGoal) //spent LESS than the average expense (= goal)
                                                 .OrderByDescending(dict => dict.Value - memberExpenseGoal) //sort by amount to give
                                                 .ToDictionary(dict => dict.Key, dict => memberExpenseGoal - dict.Value);
                    var receiverCredits = expensePerMember.Where(e => e.Value > memberExpenseGoal) //spent MORE than the average expense (= goal)
                                                 .OrderByDescending(dict => dict.Value - memberExpenseGoal) //sort by amount to receive
                                                 .ToDictionary(dict => dict.Key, dict => dict.Value - memberExpenseGoal);

                    while (giverDebts.Count > 0)
                    {
                        var giver = giverDebts.ElementAt(0);

                        for (int receiverIndex = 0; receiverIndex < receiverCredits.Count; receiverIndex++)
                        {
                            var receiver = receiverCredits.ElementAt(receiverIndex);

                            //transaction cannot exceed giver debt and exceed receiver credit 
                            decimal transferredAmount = 0;
                            if (giver.Value - receiver.Value <= 0)   // giver can't pay this much, or just enough
                                transferredAmount = giver.Value;
                            else                                     // receiver doesnt need this much
                                transferredAmount = receiver.Value;

                            receiverCredits[receiver.Key] -= transferredAmount; // amount to receive decreases
                            giverDebts[giver.Key] -= transferredAmount; // amount to give decreases

                            transactions.Add(new Debt { Debtor = giver.Key, Creditor = receiver.Key, Amount = transferredAmount });

                            if (giverDebts[giver.Key] == 0)
                            {
                                //debt cleared
                                giverDebts.Remove(giver.Key);
                                break;
                            }
                        }
                    }
                }
                

                result.IsSuccess = true;
                result.Debts = transactions;
            }
            return result;
        }

    }
}
