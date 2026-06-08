using WorldCup.API.DTOs.Poll;

namespace WorldCup.API.Services
{
    public interface IPollService
    {
        Task<PollDto> VoteAsync(int userId, VoteDto request);
        Task<PollDto?> GetMyVoteAsync(int userId);
    }
}
