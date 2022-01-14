using Howest.Prog.CoinChop.Core.Common;
using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Models;
using Howest.Prog.CoinChop.Core.Models.ExpenseGroupService;
using Howest.Prog.CoinChop.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Services
{
    public class ExpenseGroupService
    {
        private readonly IExpenseGroupRepository _expenseGroupRepository;
        private readonly ITokenService _tokenService;

        public ExpenseGroupService(
            IExpenseGroupRepository expenseGroupRepository,
            ITokenService tokenService
        )
        {
            _expenseGroupRepository = expenseGroupRepository;
            _tokenService = tokenService;
        }

        public ViewGroupResult ViewExpenseGroup(string groupToken)
        {
            //validate input
            var result = new ViewGroupResult();
            var validationResults = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(groupToken))
            {
                validationResults.Add(new ValidationResult("Token cannot be empty", new[] { nameof(groupToken) }));
            }
            else
            {
                //get group
                var group = _expenseGroupRepository.GetByToken(groupToken);
                if (group == null)
                {
                    throw new EntityNotFoundException($"A group with token '{groupToken}' was not found");
                }
                else
                {
                    //build successful result
                    result.IsSuccess = true;
                    result.Group = group;
                }
            }

            result.ValidationResults = validationResults;
            return result;
        }

        public CreateGroupResult CreateGroup(CreateGroupInput input)
        {
            //validate input
            var validator = new ModelValidator();
            var validationResults = new List<ValidationResult>();

            bool validInput = validator.TryValidateModel(input, validationResults);

            var result = new CreateGroupResult();
            result.ValidationResults = validationResults;

            if (validInput)
            {
                string token = GenerateUniqueToken();

                //add group to data store
                var groupToAdd = new ExpenseGroup
                {
                    Name = input.Name,
                    Token = token
                };

                result.IsSuccess = _expenseGroupRepository.Create(groupToAdd);

                //populate successful result
                result.GeneratedToken = token;
                result.CreatedGroupId = groupToAdd.Id;
            }
            return result;
        }

        public ReplaceTokenResult ReplaceGroupToken(ReplaceTokenInput input)
        {
            var result = new ReplaceTokenResult();
            var validator = new ModelValidator();
            var validationResults = new List<ValidationResult>();

            var group = _expenseGroupRepository.Get(input.GroupId);
            if (group == null)
            {
                throw new EntityNotFoundException($"A group with id '{input.GroupId}' was not found");
            }
            else if (group.Token != input.CurrentToken)
            {
                validationResults.Add(new ValidationResult("The provided token does not match current token", new[] { nameof(input.CurrentToken) }));
                result.ValidationResults = validationResults;
            }
            else
            {
                string newToken = GenerateUniqueToken();
                group.Token = newToken;

                result.IsSuccess = _expenseGroupRepository.Update(group);

                //build successful result
                result.NewToken = newToken;
                result.GroupId = group.Id;
            }

            return result;
        }

        public string GenerateUniqueToken()
        {
            string token;
            bool uniqueToken;
            do
            {
                token = _tokenService.GenerateToken(15);
                uniqueToken = _expenseGroupRepository.GetByToken(token) == null;
            } while (!uniqueToken);

            return token;
        }
    }
}
