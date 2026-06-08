namespace WorldCup.API.DTOs.Poll
{
    public class PollDto
    {
        public int PollId { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public DateTime VotedAt { get; set; }
    }
}
