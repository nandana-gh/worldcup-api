using FluentValidation;
using WorldCup.API.DTOs.Team;

namespace WorldCup.API.Validators.Team
{
    public class UpdateTeamDtoValidator : AbstractValidator<UpdateTeamDto>
    {
        public UpdateTeamDtoValidator()
        {
            RuleFor(x => x.TeamName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.GroupName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FlagImageUrl).NotEmpty().MaximumLength(500);
        }
    }
}
