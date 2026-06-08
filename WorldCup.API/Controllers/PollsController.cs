using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorldCup.API.DTOs.Poll;
using WorldCup.API.Services;

namespace WorldCup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;

        public PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpPost("vote")]
        public async Task<IActionResult> Vote([FromBody] VoteDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var vote = await _pollService.VoteAsync(userId, request);
                return Ok(vote);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my-vote")]
        public async Task<IActionResult> GetMyVote()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var vote = await _pollService.GetMyVoteAsync(userId);
            if (vote == null) return NotFound(new { Message = "You haven't voted yet." });
            return Ok(vote);
        }

        [HttpGet("results")]
        public async Task<IActionResult> GetResults([FromServices] IResultService resultService)
        {
            try
            {
                var isAdmin = User.IsInRole("Admin");
                var results = await resultService.GetResultsAsync(isAdmin);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
