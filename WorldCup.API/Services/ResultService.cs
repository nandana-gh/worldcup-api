using WorldCup.API.DTOs.Admin;
using WorldCup.API.DTOs.Poll;
using WorldCup.API.Models;
using WorldCup.API.Repositories;

namespace WorldCup.API.Services
{
    public class ResultService : IResultService
    {
        private readonly IRepository<Poll> _pollRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<SystemSetting> _settingsRepository;

        public ResultService(
            IRepository<Poll> pollRepository, 
            IRepository<Team> teamRepository,
            IRepository<User> userRepository,
            IRepository<SystemSetting> settingsRepository)
        {
            _pollRepository = pollRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _settingsRepository = settingsRepository;
        }

        public async Task<IEnumerable<PollResultDto>> GetResultsAsync(bool isAdmin)
        {
            var settings = await GetSettingsAsync();

            if (!isAdmin && !settings.IsResultPublished)
            {
                throw new Exception("Results not yet published");
            }

            var polls = await _pollRepository.GetAllAsync();
            var totalVotes = polls.Count();

            var allTeams = await _teamRepository.GetAllAsync();
            var activeTeams = allTeams.Where(t => t.IsActive).ToList();

            var results = activeTeams.Select(t => {
                var voteCount = polls.Count(p => p.TeamId == t.TeamId);
                return new PollResultDto
                {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    TeamCode = t.TeamCode,
                    FlagImageUrl = t.FlagImageUrl,
                    VoteCount = voteCount,
                    Percentage = totalVotes > 0 ? Math.Round((double)voteCount / totalVotes * 100, 2) : 0
                };
            }).OrderByDescending(r => r.VoteCount).ToList();

            return results;
        }

        public async Task<SystemSetting> GetSettingsAsync()
        {
            var settingsList = await _settingsRepository.GetAllAsync();
            var settings = settingsList.FirstOrDefault();
            if (settings == null)
            {
                settings = new SystemSetting { IsResultPublished = false };
                await _settingsRepository.AddAsync(settings);
                await _settingsRepository.SaveChangesAsync();
            }
            return settings;
        }

        public async Task UpdateSettingsAsync(UpdateSettingsDto request)
        {
            var settings = await GetSettingsAsync();
            settings.IsResultPublished = request.IsResultPublished;
            settings.PollClosingDate = request.PollClosingDate;
            settings.LastModified = DateTime.UtcNow;

            _settingsRepository.Update(settings);
            await _settingsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto { UserId = u.UserId, Name = u.Name, Email = u.Email, Role = u.Role });
        }

        public async Task<IEnumerable<PollDto>> GetAllPollsAsync()
        {
            var polls = await _pollRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();
            var teams = await _teamRepository.GetAllAsync();

            return polls.Select(p => new PollDto { 
                PollId = p.PollId, 
                UserId = p.UserId, 
                TeamId = p.TeamId, 
                VotedAt = DateTime.SpecifyKind(p.VotedAt, DateTimeKind.Utc),
                UserName = users.FirstOrDefault(u => u.UserId == p.UserId)?.Name ?? "Unknown",
                TeamName = teams.FirstOrDefault(t => t.TeamId == p.TeamId)?.TeamName ?? "Unknown"
            }).OrderByDescending(p => p.VotedAt).ToList();
        }
    }
}
