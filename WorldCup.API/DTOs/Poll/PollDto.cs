namespace WorldCup.API.DTOs.Poll
{
    public class PollDto
    {
        public int PollId { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public DateTime VotedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
    }
}
