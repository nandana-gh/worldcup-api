using AutoMapper;
using WorldCup.API.DTOs.Team;
using WorldCup.API.Models;

namespace WorldCup.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamDto>();
            CreateMap<CreateTeamDto, Team>();
            CreateMap<UpdateTeamDto, Team>();
            CreateMap<Poll, WorldCup.API.DTOs.Poll.PollDto>();
        }
    }
}
