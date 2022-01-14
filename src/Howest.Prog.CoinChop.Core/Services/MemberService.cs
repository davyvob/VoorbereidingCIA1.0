using Howest.Prog.CoinChop.Core.Common;
using Howest.Prog.CoinChop.Core.Entities;
using Howest.Prog.CoinChop.Core.Interfaces;
using Howest.Prog.CoinChop.Core.Models;
using Howest.Prog.CoinChop.Core.Models.MemberService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Core.Services
{
    public class MemberService

    {
        private readonly IExpenseGroupRepository _expenseGroupRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMailService _mailService;

        public MemberService(
            IExpenseGroupRepository expenseGroupRepository,
            IMemberRepository memberRepository,
            IMailService mailService
        )
        {
            _expenseGroupRepository = expenseGroupRepository;
            _memberRepository = memberRepository;
            _mailService = mailService;
        }

        public AddMemberResult AddMemberToGroup(AddMemberInput input)
        {
            //validate input
            var validator = new ModelValidator();
            var validationResults = new List<ValidationResult>();

            bool validInput = validator.TryValidateModel(input, validationResults);

            var result = new AddMemberResult();
            result.ValidationResults = validationResults;

            if (validInput)
            {
                //check if proposed group exists
                var group = _expenseGroupRepository.Get(input.GroupId);
                if (group == null)
                {
                    throw new EntityNotFoundException($"Group with id {input.GroupId} not found");
                }
                else
                {
                    //add member
                    var memberToAdd = new Member
                    {
                        Name = input.Name,
                        Email = input.Email,
                        GroupId = group.Id
                    };
                    result.IsSuccess = _memberRepository.Create(memberToAdd);

                    //populate successful result
                    result.CreatedMemberId = memberToAdd.Id;
                    result.AddedToGroupId = group.Id;

                    try
                    {
                        //send mail
                        _mailService.SendAddedToGroupMail(
                            memberToAdd.Name, memberToAdd.Email,
                            new TokenMailTemplateData { GroupName = group.Name, Token = group.Token }
                        );
                    }
                    catch(Exception exception)
                    {
                        //todo; implement logging
                        //below is commented for quick up and running demo purproses
                        //throw;
                    }
                }
            }
            return result;
        }

        public RemoveMemberResult RemoveMemberFromGroup(long memberId)
        {
            var member = _memberRepository.Get(memberId);
            if (member == null)
            {
                throw new EntityNotFoundException($"Member with id {memberId} not found");
            }
            else
            {
                var result = new RemoveMemberResult();

                result.IsSuccess = _memberRepository.Delete(member);

                //populate successful result
                result.RemovedMember = member;
                return result;
            }
        }
    }
}
