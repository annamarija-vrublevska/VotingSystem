using FluentValidation;
using VotingSystem.Api.Entities;

namespace VotingSystem.Api.Validations
{
    public class VoterEntityValidator : AbstractValidator<Voter>
    {
        public VoterEntityValidator()
        {
            RuleFor(voter => voter.UserName).EmailAddress().When(voter => string.IsNullOrEmpty(voter.Email));
        }
    }
}