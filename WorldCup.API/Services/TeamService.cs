using AutoMapper;
using WorldCup.API.DTOs.Team;
using WorldCup.API.Models;
using WorldCup.API.Repositories;

namespace WorldCup.API.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(IRepository<Team> teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task<IEnumerable<TeamDto>> GetActiveTeamsAsync()
        {
            var teams = await _teamRepository.FindAsync(t => t.IsActive);
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task<TeamDto?> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return team == null ? null : _mapper.Map<TeamDto>(team);
        }

        public async Task<TeamDto> CreateTeamAsync(CreateTeamDto request)
        {
            var existing = await _teamRepository.FindAsync(t => t.TeamCode == request.TeamCode || t.TeamName == request.TeamName);
            if (existing.Any())
            {
                throw new Exception("Team with the same name or code already exists.");
            }

            var team = _mapper.Map<Team>(request);
            team.IsActive = true;
            team.CreatedAt = DateTime.UtcNow;

            await _teamRepository.AddAsync(team);
            await _teamRepository.SaveChangesAsync();

            return _mapper.Map<TeamDto>(team);
        }

        public async Task UpdateTeamAsync(int id, UpdateTeamDto request)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null) throw new Exception("Team not found.");

            var existingName = await _teamRepository.FindAsync(t => t.TeamName == request.TeamName && t.TeamId != id);
            if (existingName.Any()) throw new Exception("Team name already in use.");

            _mapper.Map(request, team);

            _teamRepository.Update(team);
            await _teamRepository.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null) throw new Exception("Team not found.");

            // Soft delete
            team.IsActive = false;
            _teamRepository.Update(team);
            await _teamRepository.SaveChangesAsync();
        }

        public async Task ActivateTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null) throw new Exception("Team not found.");

            team.IsActive = true;
            _teamRepository.Update(team);
            await _teamRepository.SaveChangesAsync();
        }
    }
}
