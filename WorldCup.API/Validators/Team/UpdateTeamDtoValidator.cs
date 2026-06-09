using FluentValidation;
using WorldCup.API.DTOs.Team;

namespace WorldCup.API.Validators.Team
{
    public class UpdateTeamDtoValidator : AbstractValidator<UpdateTeamDto>
    {
        public UpdateTeamDtoValidator()
        {
            RuleFor(x => x.TeamName).NotEmpty().MaximumLength(100);
        }
    }
}
