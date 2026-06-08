using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldCup.API.DTOs.Admin;
using WorldCup.API.Services;

namespace WorldCup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IResultService _resultService;

        public AdminController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _resultService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("polls")]
        public async Task<IActionResult> GetPolls()
        {
            var polls = await _resultService.GetAllPollsAsync();
            return Ok(polls);
        }

        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var settings = await _resultService.GetSettingsAsync();
            return Ok(settings);
        }

        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] UpdateSettingsDto request)
        {
            await _resultService.UpdateSettingsAsync(request);
            return NoContent();
        }

        [HttpPut("reveal-results")]
        public async Task<IActionResult> RevealResults()
        {
            var settings = await _resultService.GetSettingsAsync();
            await _resultService.UpdateSettingsAsync(new UpdateSettingsDto
            {
                IsResultPublished = true,
                PollClosingDate = settings.PollClosingDate
            });
            return NoContent();
        }

        [HttpPut("hide-results")]
        public async Task<IActionResult> HideResults()
        {
            var settings = await _resultService.GetSettingsAsync();
            await _resultService.UpdateSettingsAsync(new UpdateSettingsDto
            {
                IsResultPublished = false,
                PollClosingDate = settings.PollClosingDate
            });
            return NoContent();
        }
    }
}
