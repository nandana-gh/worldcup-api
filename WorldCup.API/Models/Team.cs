using System;
using System.Collections.Generic;

namespace WorldCup.API.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string TeamCode { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string FlagImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Poll> Polls { get; set; } = new List<Poll>();
    }
}
