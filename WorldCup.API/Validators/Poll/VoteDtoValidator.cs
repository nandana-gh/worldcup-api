using FluentValidation;
using WorldCup.API.DTOs.Poll;

namespace WorldCup.API.Validators.Poll
{
    public class VoteDtoValidator : AbstractValidator<VoteDto>
    {
        public VoteDtoValidator()
        {
            RuleFor(x => x.TeamId).GreaterThan(0);
        }
    }
}
