using System;

namespace WorldCup.API.Models
{
    public class Poll
    {
        public int PollId { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User? User { get; set; }
        public Team? Team { get; set; }
    }
}
