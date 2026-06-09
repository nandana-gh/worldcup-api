using WorldCup.API.DTOs.Team;

namespace WorldCup.API.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<IEnumerable<TeamDto>> GetActiveTeamsAsync();
        Task<TeamDto?> GetTeamByIdAsync(int id);
        Task<TeamDto> CreateTeamAsync(CreateTeamDto request);
        Task UpdateTeamAsync(int id, UpdateTeamDto request);
        Task DeleteTeamAsync(int id);
        Task ActivateTeamAsync(int id);
    }
}
