using AutoMapper;
using WorldCup.API.DTOs.Poll;
using WorldCup.API.Models;
using WorldCup.API.Repositories;

namespace WorldCup.API.Services
{
    public class PollService : IPollService
    {
        private readonly IRepository<Poll> _pollRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<SystemSetting> _settingsRepository;
        private readonly IMapper _mapper;

        public PollService(
            IRepository<Poll> pollRepository, 
            IRepository<Team> teamRepository,
            IRepository<SystemSetting> settingsRepository,
            IMapper mapper)
        {
            _pollRepository = pollRepository;
            _teamRepository = teamRepository;
            _settingsRepository = settingsRepository;
            _mapper = mapper;
        }

        public async Task<PollDto> VoteAsync(int userId, VoteDto request)
        {
            var settings = (await _settingsRepository.GetAllAsync()).FirstOrDefault();
            if (settings?.PollClosingDate.HasValue == true && DateTime.UtcNow > settings.PollClosingDate.Value)
            {
                throw new Exception("Voting is closed.");
            }

            var team = await _teamRepository.GetByIdAsync(request.TeamId);
            if (team == null || !team.IsActive)
            {
                throw new Exception("Team not found or inactive.");
            }

            var existingVote = await _pollRepository.FindAsync(p => p.UserId == userId);
            if (existingVote.Any())
            {
                throw new Exception("You have already voted.");
            }

            var poll = new Poll
            {
                UserId = userId,
                TeamId = request.TeamId,
                VotedAt = DateTime.UtcNow
            };

            await _pollRepository.AddAsync(poll);
            await _pollRepository.SaveChangesAsync();

            return _mapper.Map<PollDto>(poll);
        }

        public async Task<PollDto?> GetMyVoteAsync(int userId)
        {
            var existingVote = await _pollRepository.FindAsync(p => p.UserId == userId);
            var vote = existingVote.FirstOrDefault();
            
            return vote == null ? null : _mapper.Map<PollDto>(vote);
        }
    }
}
