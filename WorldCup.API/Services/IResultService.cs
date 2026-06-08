using WorldCup.API.DTOs.Poll;
using WorldCup.API.DTOs.Admin;
using WorldCup.API.Models;

namespace WorldCup.API.Services
{
    public interface IResultService
    {
        Task<IEnumerable<PollResultDto>> GetResultsAsync(bool isAdmin);
        Task<SystemSetting> GetSettingsAsync();
        Task UpdateSettingsAsync(UpdateSettingsDto request);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<PollDto>> GetAllPollsAsync();
    }
}
