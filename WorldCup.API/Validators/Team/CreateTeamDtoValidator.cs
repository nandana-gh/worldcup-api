using FluentValidation;
using WorldCup.API.DTOs.Team;

namespace WorldCup.API.Validators.Team
{
    public class CreateTeamDtoValidator : AbstractValidator<CreateTeamDto>
    {
        public CreateTeamDtoValidator()
        {
            RuleFor(x => x.TeamName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TeamCode).NotEmpty().Length(2, 10);
        }
    }
}
